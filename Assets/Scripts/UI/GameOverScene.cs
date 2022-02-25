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

    // Based on Leveldata set text content and BGM
    void Start()
    {
        Debug.Log(
            $"Over Win {GameObject.Find("LevelData").GetComponent<LevelData>().isWin}, kill {GameObject.Find("LevelData").GetComponent<LevelData>().killCount}");
        bool isWin = GameObject.Find("LevelData").GetComponent<LevelData>().isWin;
        if (isWin)
        {
            WinText.text = "YOU WIN";
        }
        else
        {
            WinText.text = "GAME OVER";
            AudioManager.instance.Play(SoundType.SFX,"Lost");
        }

        killCountText.text = "Score: " + GameObject.Find("LevelData").GetComponent<LevelData>().killCount.ToString();
        
        AudioManager.instance.Play(SoundType.MUSIC,"GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}