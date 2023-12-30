using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    //private bool _bStart;
    //private Fade _fade;
    [SerializeField] Fade fade;
    void Start()
    {
        /*_bStart = false;
        _fade = FineObjectOfType<Fade>();
        _fade.FadeStart(_TitleStart);*/
        AudioManager.GetInstance().PlayBGM(0);
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown ( KeyCode.JoystickButton0 ) || Input.GetKeyDown ( KeyCode.JoystickButton1 ) || Input.GetKeyDown ( KeyCode.JoystickButton2 ) || Input.GetKeyDown ( KeyCode.JoystickButton3 )) 
        {
            AudioManager.GetInstance().StopBGM();
            fade.FadeIn(1f, () => SceneManager.instance.Cut());
            //SceneManager.instance.Cut();
		}
    }

}
