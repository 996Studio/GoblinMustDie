using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScene : MonoBehaviour
{
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
    
    public void SetMusicVolume()
    {
        AudioManager.instance.ChangeVolume(SoundType.MUSIC, musicVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        AudioManager.instance.ChangeVolume(SoundType.SFX, soundVolumeSlider.value);
    }
}
