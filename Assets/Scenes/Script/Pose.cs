using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // ポーズメニューを非表示
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // あなたのポーズトリガーに合わせて変更
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // ゲーム時間を停止
        Debug.Log("stop");
        pauseMenuUI.SetActive(true); // ポーズメニューを表示
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // ゲーム時間を再開
        pauseMenuUI.SetActive(false); // ポーズメニューを非表示
    }
}