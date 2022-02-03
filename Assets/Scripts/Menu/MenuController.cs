using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Level1");
    }
     public void OnOptionsButton_Pressed()
     {
         SceneManager.LoadScene("OptionsScene");
     }
     public void OnIntroductionButton_Pressed()
     {
         SceneManager.LoadScene("IntroductionScene");
     }
            
    public void OnBackButton_Pressed()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnQuitButton_Pressed()
    {
        Application.Quit();
    }
    
}
