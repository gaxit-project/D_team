using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance() //シングルトン
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Title() //タイトル
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }

    public void Cut() //タイトル
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("cut");
    }

    public void GamePlay() //ゲーム画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void GameOver() //ゲームオーバー画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
    }
    
    public void GameClear() //クリア画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("clear");
    }

    public void Game1() //Game1
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ1",LoadSceneMode.Additive);
    }

    public void Game2() //Game2
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ2",LoadSceneMode.Additive);
    }

    public void Game3() //Game3
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ3",LoadSceneMode.Additive);
    }

    public void Game4() //Game4
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ステージ4",LoadSceneMode.Additive);
    }

    public void Game1End() //Game1End
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ステージ1");
    }

    public void Game2End() //Game2End
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ステージ2");
    }

    public void Game3End() //Game3End
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ステージ3");
    }

    public void Game4End() //Game4End
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ステージ4");
    }

    public void EndGame() //ゲーム終了
    {
        #if UNITY_EDITOR //unity内でゲーム時
            UnityEditor.EditorApplication.isPlaying = false;
        #else //ビルドされたゲームの時
            Application.Quit();
        #endif
    }
}
