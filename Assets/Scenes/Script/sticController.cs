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
    int sum ;
    int add ;
    [SerializeField] Fade fade;
    bool con = false;
    
    void Start()
    {
        con = false;
        this.rigid = GetComponent<Rigidbody>();
        Application.targetFrameRate=30;
        Stic_status = false;
        sum = PlayerOperate.meter_sum;
        add = PlayerOperate.meter_add;
        fade.FadeOut(1f, () => con = true);
        /*if(con == true)
        {
            Fade.startFade = false;
        }*/
    }
    void Update()
    {
        if(con == true)
        {
            float speedx=Mathf.Abs(this.rigid.velocity.x);
            float speedy=Mathf.Abs(this.rigid.velocity.y);
            //上下移動
            if(speedy<this.maxSpeed)
            {
                this.rigid.AddForce(transform.forward * Input.GetAxis("JoyVertical") * this.SlideForce*-1);
            }
            //左右移動
            if(speedx<this.maxSpeed)
            {
                this.rigid.AddForce(transform.right * Input.GetAxis("JoyHorizontal") * this.SlideForce*-1);
            }
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
        if (Stic_status == true)// イライラ棒終了
        {
            if (f == true)//イライラ棒成功
            {
                Debug.Log("イライラ棒成功");
                AudioManager.GetInstance().PlaySound(4);
                sum = sum / 3;
                add = sum;
                //PlayerOperate.Geri_Slider.value = sum;
            }else{// 失敗
                Debug.Log("イライラ棒失敗");
                AudioManager.GetInstance().PlaySound(5);
            }
            PlayerOperate.meter_sum = sum;
            PlayerOperate.meter_add = add;
        }
        //
        if ( PlayerOperate.Volt == 1){
            SceneManager.instance.Game3End();
        }else if( PlayerOperate.Volt == 2){
            SceneManager.instance.Game1End();
        }else if( PlayerOperate.Volt == 3){
            SceneManager.instance.Game2End();
        }else if( PlayerOperate.Volt >= 4){
            SceneManager.instance.Game4End();
        }
        PlayerOperate.Volt_status = 0;
        PlayerOperate.Volt_sta = false;
        Stic_status = false;
        if (f == true)//イライラ棒成功
        {
            PlayerOperate.Volt++;
        }
    }
}