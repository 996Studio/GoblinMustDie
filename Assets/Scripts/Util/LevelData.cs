////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: LevelData.cs
//Author: Zihan Xu
//Student Number: 101288760
//Last Modified On : 2/6/2022
//Description : Class for Level Data
//Revision History:
//2/6/2022: Use this class to store data of the level
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public bool isWin;
    public int killCount;
    public int levelIndex;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}