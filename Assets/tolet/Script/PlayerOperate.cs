using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerOperate : MonoBehaviour
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
    public static bool pause_status = false;
    public static int pt = 0;
    public static float BGM_Vo = 1.00f;
    public static float SE_Vo = 1.00f;
    int rd;
    //イライラ棒
    public static int Volt = 1;
    public static int Volt_status = 0;
    public static bool Volt_sta;
    // 下痢メーター
    public Slider Geri_Slider;
    public float T = 1.0f;
    float f = 1.0f;
    int pn;
    //int geri = 0;

    //下痢タイマー
    public float geri_timer;
    //下痢余命
    public float life_timer;
    public int geriMAX = 1000;
    //テキストの数値化
    public static int meter_sum;
    public static int meter_add = 1;
    // テンションメーター
    [SerializeField] Slider Tension_Slider;
    public float temsion_timer;
    public float span = 3.0f;
    // テンション画像
    [SerializeField] Sprite imageGood;
    [SerializeField]  Sprite imageBut;
    [SerializeField]  Image myPhoto;
    //悲鳴
    bool isCalledOnce = false;
    // フェード
    [SerializeField] Fade fade;
    void Start()
    {
        Application.targetFrameRate = 20;

        //BGM
        rd = Random.Range(1, 3);
        AudioManager.GetInstance().PlayBGM(rd);

        //BGM 音量
        Geri_Slider.maxValue = geriMAX;
        _target.enabled = false;

        // テンションメーターの周期
        temsion_timer = 0.0f;

        //時間制御
        geri_timer = 0.0f;
        life_timer = 0.0f;

        f = 1.0f / T;

        // テンションの画像
        myPhoto = GameObject.Find("/Canvas/tyousi").GetComponent<Image>();
        myPhoto.enabled = false;

        //イライラ棒の状態
        Volt = 1;
        Volt_sta = false;

        //下痢の初期化
        meter_add = 0;
        meter_sum = 0;
        Geri_Slider.value = meter_sum;
    }
    void Update()
    {
        if (pause_status == false){//ポーズ中か
            //死亡した後動けないように
            if (death== false)
            {
                Move();
                Move_PCkey();
            }
            defecating();
            Volt_Tackle();
            Volt_Tackle_meter();
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
                moveDirection = new Vector3(0, 0, Input.GetAxis("JoyVertical") * Walk_speed);
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
                pause_status = true;
                pauseUIInstance = GameObject.Instantiate (pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            } else {
                pause_status = false;
                Destroy (pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }
    //電撃イライラ棒
    void Volt_Tackle()
    {
        if (Volt_sta == false)
        {
            if (Input.GetAxis("JoyVertical") == 0)
            {
                if (Input.GetButtonDown("JoyRight"))
                {
                    Volt_status = 1;
                    Volt_sta = true;
                    Debug.Log("Volt: " + Volt);
                    Debug.Log("Is Stic_status true? " + sticController.Stic_status);
                    if ( Volt == 1){
                        fade.FadeIn(1f, () => SceneManager.instance.Game3());
                    }else if( Volt == 2){
                        fade.FadeIn(1f, () => SceneManager.instance.Game1());
                    }else if( Volt == 3){
                        fade.FadeIn(1f, () => SceneManager.instance.Game2());
                    }else if( Volt >= 4){
                        fade.FadeIn(1f, () => SceneManager.instance.Game4());
                    }
                }
            }
            if (sticController.Stic_status == true)// イライラ棒終了
            {
                //イライラ棒成功
                if (sticController.f == true)
                {
                    Debug.Log("イライラ棒成功");
                    AudioManager.GetInstance().PlaySound(4);
                    meter_sum = meter_sum / 3;
                    meter_add = meter_sum;
                    Geri_Slider.value = meter_sum;
                }else{// 失敗
                    Debug.Log("イライラ棒失敗");
                    AudioManager.GetInstance().PlaySound(5);
                }
                if ( Volt == 1){
                    SceneManager.instance.Game3End();
                }else if( Volt == 2){
                    SceneManager.instance.Game1End();
                }else if( Volt == 3){
                    SceneManager.instance.Game2End();
                }else if( Volt >= 4){
                    SceneManager.instance.Game4End();
                }
                Volt_status = 0;
                Volt_sta = false;
                Volt++;
                sticController.Stic_status = false;
            }
        }
    }
    void Volt_Tackle_meter(){
        if (sticController.f == true){
            Geri_Slider.value = meter_sum;
        }
    }
    //脱糞
    void defecating ()
    {
        // テンションメーター
        temsion_timer += Time.deltaTime;
        if (temsion_timer > span)// 3秒ごとに変化
        {
            int tension_rnd = Random.Range(1, 11);// 1～10の範囲でランダムな整数値が返る
            if (tension_rnd <= 5){
                myPhoto.enabled = true;
                myPhoto.sprite = imageGood;
                pn = tension_rnd;
            }else{
                myPhoto.enabled = true;
                myPhoto.sprite = imageBut;
                pn = tension_rnd;
            }
            Tension_Slider.value = tension_rnd;
            temsion_timer = 0.0f;
        }

        // 下痢メーター
        geri_timer += Time.deltaTime;
        life_timer += Time.deltaTime;
        if (death == false)
        {
            if ( Volt_status == 0)// イライラ棒中以外
            {
                if (life_timer > span)//3秒ごとに腹が勝手に悪くなる
                {
                    meter_add += 2;
                    meter_sum = meter_add;
                    Geri_Slider.value = meter_sum;
                    life_timer = 0.0f;
                }

                if (Input.GetAxis("JoyVertical") != 0)//動いているとき加算
                {
                    if (geri_timer > 1)
                    {
                        if (Input.GetButton("JoyDown") || Input.GetButton("JoyA"))
                        {
                            meter_add += 3 * pn;
                        }else{
                            meter_add += 1 * ((pn / 3) * 2);
                        }
                        geri_timer = 0.0f;
                    }
                }
            }
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
            //Debug.Log("ピンチBGM1");
            if (pinchBGM == true)
            {
                //ピンチSE
                AudioManager.GetInstance().PlayBGM(3);
                //Debug.Log("ピンチBGM2");
            }
            pinchBGM = false;
        }
    }
    //通常時のBGM
    void NormalMusic()
    {
        if (pinch == false)
        {
            //Debug.Log("ノーマルBGM1");
            if (pinchBGM == false)
            {
                //ピンチSE
                AudioManager.GetInstance().PlayBGM(rd);
                //Debug.Log("ノーマルBGM2");
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