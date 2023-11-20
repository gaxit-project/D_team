using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerOperate : MonoBehaviour
{

    //コントローラに関する初期設定
    public float speed = 2.5f;
    public float Dash_speed = 10.0f;
    public float gravity = 20.0f;
    public float protateSpeed = 1.5f;
    public float Dash_protateSpeed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;

    //アニメーション
    public Animator anim;

    //ゲームオーバー切り替え時間
    private bool isScriptPaused = true;

    //死亡時に動けないようにピンチで画面点滅
    bool death = false;
    bool pinch = false;

    bool pinchBGM = true;

    // 点滅させる対象
    [SerializeField]  private Behaviour _target;
    // 点滅周期[s]
    [SerializeField] float _cycle = 1;
    private double _time;


    [SerializeField]
	//　ポーズした時に表示するUIのプレハブ
	private GameObject pauseUIPrefab;
	//　ポーズUIのインスタンス
	private GameObject pauseUIInstance;

    //ポーズ中か判定
    int p = 0;

    public static int pt = 0;

    public static float BGM_Vo = 1.00f;
    public static float SE_Vo = 1.00f;

    int rd;

    //イライラ棒
    public static int Volt = 1;
    public static int Volt_status = 0;

    // 下痢メーター
    [SerializeField] Slider Geri_Slider;

    public float T = 1.0f;
    float f = 1.0f;

    float f2 ;
    float sin = 0.0f;
    float sin2 = 0.0f;

    int pn;

    //int geri = 0;
    public int geriMAX = 100;
    //テキストの数値化
    int meter_sum;
    int meter_add = 1;

    // 調子画像
    [SerializeField] Sprite imageGood;
    [SerializeField]  Sprite imageBut;
    [SerializeField]  Image myPhoto;

    //悲鳴
    bool isCalledOnce = false;

    void Start()
    {
        Application.targetFrameRate = 20;
        //BGM
        rd = Random.Range(1, 3);
        AudioManager.GetInstance().PlayBGM(rd);
        Geri_Slider.maxValue = geriMAX;
        _target.enabled = false;
        
        f = 1.0f / T;
        Volt = 1;
        myPhoto = GameObject.Find("/Canvas/tyousi").GetComponent<Image>();
        myPhoto.enabled = false;
    }
    void Update()
    {
        if (p == 0){//ポーズ中か
            //死亡した後動けないように
            if (death== false)
            {
                Move();
                Move_PCkey();
            }
            defecating();
            Volt_Tackle();
            //ピンチで画面点滅
            if (pinch == true)
            {
                pincheffect();
            }
        }
        Pause();
    }

    void Move()
    {

        // CharacterController取得
        CharacterController controller = this.gameObject.GetComponent<CharacterController>();

        //
        if (Input.GetButton("JoyDown") || Input.GetButton("JoyA"))
        {
            //   ]
            transform.Rotate(0, -Input.GetAxis("JoyHorizontal") * Dash_protateSpeed, 0);
            Debug.Log("ダッシュボタン押されてる");

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
            //歩きモーション(ゲージたまるとモーション変化)
            if (Input.GetAxis("JoyVertical") != 0 )
            {
                //ココの値で変化
                if (pinch == true)
                {
                    Dash_speed = 1f;
                    anim.SetTrigger("pinch");
                }else{
                    Dash_speed = 10f;
                    anim.SetTrigger("walk");
                }
            }
            transform.Rotate(0, -Input.GetAxis("JoyHorizontal") * protateSpeed, 0);

            //
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("JoyVertical"));
                moveDirection = this.gameObject.transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    public void Move_PCkey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("run");
            transform.Translate(0, 0, 0.1f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger("run");
            transform.Rotate(0, 1f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetTrigger("run");
            transform.Rotate(0, -1f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (pauseUIInstance == null) 
            {
				pauseUIInstance = GameObject.Instantiate (pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
			} else {
				Destroy (pauseUIInstance);
				Time.timeScale = 1f;
			}
        }
    }


    void Pause ()
    {
        if (Input.GetButtonDown("JoyMinus") || Input.GetButtonDown("JoyPlus"))
        {
            if (pauseUIInstance == null) 
            {
                p = 1;
				pauseUIInstance = GameObject.Instantiate (pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
			} else {
                p = 0;
				Destroy (pauseUIInstance);
				Time.timeScale = 1f;
			}
        }
        /*if (pt == 1)
        {
            p = 0;
			Destroy (pauseUIInstance);
			Time.timeScale = 1f;
        }*/
    }

    //電撃イライラ棒
    void Volt_Tackle()
    {
        if (Input.GetAxis("JoyVertical") == 0)
        {
            if (Input.GetButtonDown("JoyRight"))
            {
                Volt_status = 1;
                if ( Volt == 1){
                    SceneManager.instance.Game1();
                }else if( Volt == 2){
                    SceneManager.instance.Game2();
                }else if( Volt == 3){
                    SceneManager.instance.Game3();
                }else if( Volt == 4){
                    SceneManager.instance.Game4();
                }
            }
        }
        if (sticController.test == 1)
        {
            //イライラ棒成功
            if (sticController.f == true)
            {
                AudioManager.GetInstance().PlaySound(4);
                meter_sum = meter_sum / 3;
                meter_add = meter_sum;
                Geri_Slider.value = meter_sum;
            }else{// 失敗
                AudioManager.GetInstance().PlaySound(5);
            }

            if ( Volt == 1){
                SceneManager.instance.Game1End();
            }else if( Volt == 2){
                SceneManager.instance.Game2End();
            }else if( Volt == 3){
                SceneManager.instance.Game3End();
            }else if( Volt == 4){
                SceneManager.instance.Game4End();
            }
            Volt_status = 0;
            Volt++;
            sticController.test = 0;
        }
    }
    
    //脱糞
    void defecating ()
    {
        int rnd = Random.Range(5, 11);// ※ 5～10の範囲でランダムな整数値が返る
        f2 = 1.0f / rnd;
        sin = Mathf.Sin(2 * Mathf.PI * f * Time.time);
        sin2 = Mathf.Sin(2 * Mathf.PI * f2 * Time.time);
        //Debug.Log(sin);

        if (sin2 > 0.5) {
            myPhoto.enabled = true;
            myPhoto.sprite = imageGood;
            pn = 1;
        } else if (sin2 > -0.5) {
            myPhoto.enabled = true;
            myPhoto.sprite = imageGood;
            pn = 2;
        }
        else { 
            myPhoto.enabled = true;
            myPhoto.sprite = imageBut;
            pn = 4;
        }

        if (death == false)
        {
            if ( Volt_status == 0)
            {// イライラ棒中以外
                //動いているとき加算
                if (Input.GetAxis("JoyVertical") != 0)
                {
                    if (sin >= 0.99)
                    {
                        if (Input.GetButton("JoyDown") || Input.GetButton("JoyA"))
                        {
                            meter_add += 3 * pn;
                        }else{
                            meter_add += 1 * pn;
                        }
                    }
                }else{
                    if (sin >= 0.9999)
                    {
                        meter_add += 1;
                    }
                }
            }
            //text -> int
            meter_sum = meter_add;  
            Geri_Slider.value = meter_sum;

            //ピンチ判定
            if (meter_sum >= geriMAX*0.8)
            {
                pinch = true;
                pinchMusic();
            }
            else
            {
                _target.enabled = false;
                pinch = false;
                NormalMusic();
            }

            //MAXを超えたらGameOver
            if (meter_sum >= geriMAX)
            {
                //死亡モーションと動けないように、ピンチ時の画面点滅解除
                death = true;
                pinch = false;
                //_target.enabled = false;
                anim.SetTrigger("death");
                if (!isCalledOnce) {
                    Debug.Log("aaaa");
                    isCalledOnce = true;
                    AudioManager.GetInstance().PlaySound(3);
                }
                StartCoroutine(PauseScriptForSeconds(2f));
            }
        }
    }

    void pincheffect()
    {
        //ピンチになるほど早く
        _cycle = 2 - (meter_sum * 0.01f);

        // 内部時刻を経過させる
        _time += Time.deltaTime;

        // 周期cycleで繰り返す値の取得
        // 0～cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat((float)_time, _cycle);

        // 内部時刻timeにおける明滅状態を反映
        _target.enabled = repeatValue >= _cycle * 0.5f;
    }

    //ピンチ時にBGMをならす
    void pinchMusic()
    {
        if (pinch == true)
        {
            Debug.Log("ピンチBGM1");
            if (pinchBGM == true)
            {
                //ピンチSE
                AudioManager.GetInstance().PlayBGM(3);
                Debug.Log("ピンチBGM2");
            }
            pinchBGM = false;
        }
    }

    //通常時のBGM
    void NormalMusic()
    {
        if (pinch == false)
        {
            Debug.Log("ノーマルBGM1");
            if (pinchBGM == false)
            {
                //ピンチSE
                AudioManager.GetInstance().PlayBGM(rd);
                Debug.Log("ノーマルBGM2");
            }
            pinchBGM = true;
        }
    }

    private IEnumerator PauseScriptForSeconds(float seconds)
    {
        isScriptPaused = true;
        yield return new WaitForSeconds(seconds);
        isScriptPaused = false;
        if (isScriptPaused == false)
        {
            // 通常のスクリプト処理をここに記述
            Debug.Log("GameOver");
            SceneManager.instance.GameOver();
        }
    }
}