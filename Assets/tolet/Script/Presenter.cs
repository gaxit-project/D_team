using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Presenter : MonoBehaviour
{

    [SerializeField] Slider BGM_Slider;
    [SerializeField] Text BGM_VolumeText;

    [SerializeField] Slider SE_Slider;
    [SerializeField] Text SE_VolumeText;


    //　選択中のIcon
    [SerializeField] private Behaviour BGM_Icon;
    [SerializeField] private Behaviour SE_Icon;
    [SerializeField] private Behaviour title_Icon;
    [SerializeField] private Behaviour END_Icon;

    Slider BGM_slider;
    Slider SE_slider;
    Button title_button;
	Button END_button;

    int sele = 0;
    int sele_index = 4;


    void Start()
    {
        // ボタンコンポーネントの取得
        BGM_slider = GameObject.Find ("/Pause_UI/Panel/BGMview/BGM_Slider").GetComponent<Slider> ();
        SE_slider = GameObject.Find ("/Pause_UI/Panel/SEview/SE_Slider").GetComponent<Slider> ();
		title_button = GameObject.Find ("/Pause_UI/Panel/title_Button").GetComponent<Button> ();
		END_button = GameObject.Find ("/Pause_UI/Panel/Esc_Button").GetComponent<Button> ();

        // 画像コンポーネントの取得
        //BGM_Icon = GameObject.Find ("/Pause_UI/Panel/bgm_Icon").GetComponent<Image> ();
        //SE_Icon = GameObject.Find ("/Pause_UI/Panel/se_Icon").GetComponent<Image> ();
        //title_Icon = GameObject.Find ("/Pause_UI/Panel/Title_Icon").GetComponent<Image> ();
        //END_Icon = GameObject.Find ("/Pause_UI/Panel/Esc_Icon").GetComponent<Image> ();

        //Sliderの値を変える
        BGM_Slider.value = PlayerOperate.BGM_Vo;
        SE_Slider.value = PlayerOperate.SE_Vo;

        // 最初に選択状態にしたいボタンの設定
		BGM_slider.Select ();
        SE_Icon.enabled = false;
    }

    void Update () 
    {
        PauseProcess ();
    }

    //BGMスライダー変化を受け取りUIの変化
    public void OnChengedBGMSlider()
    {
        AudioManager.GetInstance().BGMVolume = BGM_Slider.value;
        BGM_VolumeText.text = string.Format("{0:0.00}", BGM_Slider.value);
    }

    //SEスライダー変化を受け取りUIの変化
    public void OnChengedSESlider()
    {
        AudioManager.GetInstance().SEVolume = SE_Slider.value;
        SE_VolumeText.text = string.Format("{0:0.00}", SE_Slider.value);
    }

    //titleボタンを押したらtitleへ
    public void OnButtontitle()
    {
        PlayerOperate.pt = 1;
        SceneManager.instance.Title();
    }

    //ゲーム終了ボタンを押したらゲーム終了
    public void OnButtonGameEND()
    {
        SceneManager.instance.EndGame();
    }

    public void PauseProcess ()
    {
        if(Input.GetButtonDown("JoyZL"))
        {
            sele++;
            if(0 == sele % sele_index)
            {
                BGM_slider.Select ();
            }else if (1 == sele % sele_index){
                SE_slider.Select ();
            }else if (2 == sele % sele_index){
                title_button.Select ();
            }else{
                END_button.Select ();
            }
        }

        if(0 == sele % sele_index)
        {
            BGM_Slider.value += (-Input.GetAxis("JoyHorizontal") / 100);
            PlayerOperate.BGM_Vo = BGM_Slider.value;
        }else if (1 == sele % sele_index){
            SE_Slider.value += (-Input.GetAxis("JoyHorizontal") / 100);
            PlayerOperate.SE_Vo = SE_Slider.value;
        }else if (2 == sele % sele_index){
            if (Input.GetButtonDown("JoyDown"))
            {
                Time.timeScale = 1f;
                PlayerOperate.pause_status = false;
                PlayerOperate.Volt_status = 0;
                PlayerOperate.Volt_sta = false;
                PlayerOperate.meter_add = 0;
                PlayerOperate.meter_sum = 0;
                OnButtontitle();
            }
        }else{
            if (Input.GetButtonDown("JoyDown"))
            {
                OnButtonGameEND();
            }
        }
    }
    /*public void Icon_select (int n)
    {
        Debug.Log("aaaa");
        if(n == 1)
        {
            if (BGM_IconInstance == null) 
            {
				BGM_IconInstance = GameObject.Instantiate (BGM_Icon) as GameObject;
			} else {
				Destroy (BGM_IconInstance);
			}
        }else if(n == 2){
            if (SE_IconInstance == null) 
            {
				SE_IconInstance = GameObject.Instantiate (SE_Icon) as GameObject;
			} else {
				Destroy (SE_IconInstance);
			}
        }else if(n == 3){
            if (title_IconInstance == null) 
            {
				title_IconInstance = GameObject.Instantiate (title_Icon) as GameObject;
			} else {
				Destroy (title_IconInstance);
			}
        }else if(n == 4){
            if (END_IconInstance == null) 
            {
				END_IconInstance = GameObject.Instantiate (END_Icon) as GameObject;
			} else {
				Destroy (END_IconInstance);
			}
        }
    }*/
}
