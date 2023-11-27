using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CutScene : MonoBehaviour
{
    public float Cut_timer;
    private void Start()
    {
        Cut_timer = 0.0f;
    }
    void Update()
    {
        Cut_timer += Time.deltaTime;
        if (Cut_timer > 3)
        {
            Cut_timer = 0.0f;
            SceneManager.instance.GamePlay();
        }
    }
}
