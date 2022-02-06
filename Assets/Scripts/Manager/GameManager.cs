using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public PauseMenu PauseMenu;
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play(SoundType.MUSIC,"BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        PauseMenu.ShowPauseMenu();
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        PauseMenu.DestroyPauseMenu();
    }
}
