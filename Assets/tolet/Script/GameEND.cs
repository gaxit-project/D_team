using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEND : MonoBehaviour
{
    void Start()
    {
        AudioManager.GetInstance().StopBGM();
        AudioManager.GetInstance().PlaySound(0);
    }
    void Update()
    {
        
        //SceneManagerのtitle偏移を呼び出す
        if (Input.GetKeyDown ( KeyCode.JoystickButton15 ) ) 
        {
            SceneManager.instance.Title();
		}
        if (Input.GetKeyDown ( KeyCode.JoystickButton13 ) || Input.GetKeyDown ( KeyCode.Escape )) 
        {
            SceneManager.instance.EndGame();
		}
    }
}
