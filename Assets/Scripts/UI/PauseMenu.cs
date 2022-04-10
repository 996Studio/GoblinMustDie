using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetMusicVolume();
        SetSoundVolume();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    public void SetMusicVolume()
    {
        AudioManager.instance.ChangeVolume(SoundType.MUSIC, musicVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        AudioManager.instance.ChangeVolume(SoundType.SFX, soundVolumeSlider.value);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
    }

}
