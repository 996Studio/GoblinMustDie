using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
    }

    public void DestroyPauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }
}
