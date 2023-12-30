using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningPlay : MonoBehaviour
{
    //コントローラの初期設定
    public float speed = 2.5f;
    public float Dash_speed = 10.0f;
    public float Walk_speed = 1.15f;
    public float gravity = 20.0f;
    public float protateSpeed = 1.5f;
    public float Dash_protateSpeed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    //アニメーション
    public Animator anim;
    //死亡時に動けないようにピンチで画面点滅
    public static bool pinch = false;
    public void Start()
    {
        pinch = false;
    }
    public void Update()
    {
        //死亡した後動けないように
        if (pinch== false)
        {
            Move();
        }
    }
    public void Move()
    {
        // CharacterController取得
        CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        //
        if (Input.GetButton("JoyDown") || Input.GetButton("JoyA"))
        {
            //   ]
            transform.Rotate(0, -Input.GetAxis("JoyHorizontal") * Dash_protateSpeed, 0);
            //走りモーション(ゲージたまるとモーション変化)
            if (Input.GetAxis("JoyVertical") != 0 )
            {
                //ココの値で変化
                if (pinch == true)
                {
                    Dash_speed = 1f;
                    anim.SetTrigger("pinch");
                }else{
                    anim.SetTrigger("run");
                }
            }
            // O E   ɐi
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("JoyVertical"));
                moveDirection = this.gameObject.transform.TransformDirection(moveDirection);
                moveDirection *= Dash_speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            if (pinch == true)
            {
                Dash_speed = 1f;
                anim.SetTrigger("pinch");
            }else{
                Dash_speed = 10f;
                anim.SetTrigger("walk");
            }
            transform.Rotate(0, -Input.GetAxis("JoyHorizontal") * protateSpeed, 0);
            //
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("JoyVertical") * Walk_speed);
                moveDirection = this.gameObject.transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
