using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sticController : MonoBehaviour
{
    
	Rigidbody rigid;
    float SlideForce = 10f;
    float maxSpeed=10f;

    public static bool Stic_status = false;
    public static bool f = false;

    void Start()
    {
        this.rigid = GetComponent<Rigidbody>();
        Application.targetFrameRate=30;
        Stic_status = false;
    }

    void Update()
    {
        float speedx=Mathf.Abs(this.rigid.velocity.x);
        float speedy=Mathf.Abs(this.rigid.velocity.y);

        //上下移動
        if(speedy<this.maxSpeed){
            this.rigid.AddForce(transform.forward * Input.GetAxis("JoyVertical") * this.SlideForce*-1);
        }

        //左右移動
        if(speedx<this.maxSpeed){
            this.rigid.AddForce(transform.right * Input.GetAxis("JoyHorizontal") * this.SlideForce*-1);
        }
    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("クリア");
            f = true;
        }else{
            Debug.Log("失敗");
            f = false;
        }
        Stic_status = true;
    }
}
