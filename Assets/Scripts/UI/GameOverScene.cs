////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: GameOverScene.cs
//Author: Zihan Xu
//Student Number: 101288760
//Last Modified On : 2/6/2022
//Description : Class for GameOverScene
//Revision History:
//2/6/2022: Implement feature of changing scenes and setting level data
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text WinText;
    public TMP_Text killCountText;
    public Button nextLevelButton;

    private int nextLevelIndex;

    // Based on Leveldata set text content and BGM
    void Start()
    {
        Debug.Log(
            $"Over Win {GameObject.Find("LevelData").GetComponent<LevelData>().isWin}, kill {GameObject.Find("LevelData").GetComponent<LevelData>().killCount}");
        bool isWin = GameObject.Find("LevelData").GetComponent<LevelData>().isWin;
        nextLevelIndex = GameObject.Find("LevelData").GetComponent<LevelData>().levelIndex + 1;
        
        if (isWin)
        {
            WinText.text = "YOU WIN";
            if (nextLevelIndex <= GameSetting.instance.levelNumber)
            {
                nextLevelButton.gameObject.SetActive(true);
            }
        }
        else
        {
            WinText.text = "GAME OVER";
            nextLevelButton.gameObject.SetActive(false);
            AudioManager.instance.Play(SoundType.SFX,"Lost");
        }

        killCountText.text = "Score: " + GameObject.Find("LevelData").GetComponent<LevelData>().killCount.ToString();
        
        AudioManager.instance.Play(SoundType.MUSIC,"GameOver");
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void GotoMainMenu()
    {
        AudioManager.instance.Stop(SoundType.MUSIC);
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToNextLevel()
    {
        AudioManager.instance.Stop(SoundType.MUSIC);
        SceneManager.LoadScene("Level" + nextLevelIndex.ToString());
    }
}