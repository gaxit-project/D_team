using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CutScene : MonoBehaviour
{
    private void Start()
    {
        // コルーチンの起動
        StartCoroutine(DelayCoroutine());
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 3秒間待つ
        yield return new WaitForSeconds(3);
        SceneManager.instance.GamePlay();
    }
}
