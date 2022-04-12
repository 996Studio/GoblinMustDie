using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting instance;

    [Header("Options")]
    public float musicVolume;
    public float soundVolume;

    public int maxUnlockedLevelIndex;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.ChangeVolume(SoundType.SFX, soundVolume);
        AudioManager.instance.ChangeVolume(SoundType.MUSIC, musicVolume);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
