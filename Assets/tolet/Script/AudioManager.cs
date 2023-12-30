using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //音ファイル
    [SerializeField] AudioClip[] SE_List;
    [SerializeField] AudioClip[] BGM_List;

    //音の鳴らし方指定
    [SerializeField] AudioSource audioSorceBGM;
    [SerializeField] AudioSource audioSorceSE;

    public float BGMVolume //BGMボリューム
    {
        get { return audioSorceBGM.volume; }
        set { audioSorceBGM.volume = value; }
    }

    public float SEVolume //SEボリューム
    {
        get { return audioSorceSE.volume; }
        set { audioSorceSE.volume = value; }
    }

    static AudioManager Instance = null;

    public static AudioManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<AudioManager>();
        }
        return Instance;
    }

    private void Awake() //シングルトン
    {
        if (this != GetInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(int index) //SE再生
    {
        audioSorceSE.PlayOneShot(SE_List[index]);
    }

    public void PlayBGM(int index) //BGM再生
    {
        audioSorceBGM.clip = BGM_List[index];
        audioSorceBGM.Play();
    }

    public void StopBGM() //BGM停止
    {
        audioSorceBGM.Stop();
    }

}
