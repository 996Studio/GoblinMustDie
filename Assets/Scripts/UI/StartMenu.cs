using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject introduction;
    public GameObject options;
    public GameObject credits;

    public Animator animator;

    [Header("Options")] 
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowStartMenu();
        Time.timeScale = 1f;
        AudioManager.instance.Play(SoundType.MUSIC,"Menu");
        
        musicVolumeSlider.value = GameSetting.instance.musicVolume;
        soundVolumeSlider.value = GameSetting.instance.soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (options.gameObject.activeSelf)
        {
            SetMusicVolume();
            SetSoundVolume();
        }
    }

    public void ShowStartMenu()
    {
        startMenu.gameObject.SetActive(true);
        introduction.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }

    public void ShowIntroduction()
    {
        startMenu.gameObject.SetActive(false);
        introduction.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }

    public void ShwoOptions()
    {
        startMenu.gameObject.SetActive(false);
        introduction.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
    }

    public void ShowCredits()
    {
        startMenu.gameObject.SetActive(false);
        introduction.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }

    public void SetMusicVolume()
    {
        //Debug.Log("Set music volume");
        AudioManager.instance.ChangeVolume(SoundType.MUSIC, musicVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        AudioManager.instance.ChangeVolume(SoundType.SFX, soundVolumeSlider.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
       Application.Quit();
    }
}
