using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipMovie : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown ( KeyCode.JoystickButton0 ) || Input.GetKeyDown ( KeyCode.JoystickButton1 ) || Input.GetKeyDown ( KeyCode.JoystickButton2 ) || Input.GetKeyDown ( KeyCode.JoystickButton3 )) 
        {
            SceneManager.instance.GamePlay();
		}
    }
}
