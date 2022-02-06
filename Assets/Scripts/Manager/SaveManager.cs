using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class SaveManager : MonoBehaviour
{
    private static string SAVE_FOLDER;

    private void Awake()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves/";
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public SaveData CreateSaveData()
    {
        SaveData save = new SaveData();
        save.coin = ResourceManager.Instance().Coin;
        save.wood = ResourceManager.Instance().Wood;
        save.rock = ResourceManager.Instance().Rock;


        save.towerNum = 0;
        foreach (var node in NodeManager.instance.NodeList)
        {
            Debug.Log(node.TowerType);
            save.towerTypeList.Add(node.TowerType);
            if (node.TowerType == TowerType.NULL)
            {
                save.levelList.Add(0);
            }
            else
            {
                save.levelList.Add(node.Tower.GetComponent<BaseTower>().Level);
            }

            save.towerNum++;
        }
        return save;
    }

    public void SaveByJson()
    {
        SaveData save = CreateSaveData();
        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(SAVE_FOLDER + "JsonData.txt");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("save");
    }

    public void LoadByJson()
    {
        if (File.Exists(SAVE_FOLDER + "JsonData.txt"))
        {
            StreamReader sr = new StreamReader(SAVE_FOLDER + "JsonData.txt");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            SaveData save = JsonUtility.FromJson<SaveData>(JsonString);

            ResourceManager.Instance().Coin = save.coin;
            ResourceManager.Instance().Wood = save.wood;
            ResourceManager.Instance().Rock = save.rock;

            for (int i = 0; i < save.towerNum - 1; i++)
            {
                NodeManager.instance.LoadTower(NodeManager.instance.NodeList[i], save.towerTypeList[i],
                    save.levelList[i]);
            }

            Debug.Log("Load" + ResourceManager.Instance().Coin);
        }
        else
        {
            Debug.Log("No Save Data");
        }
    }
}
