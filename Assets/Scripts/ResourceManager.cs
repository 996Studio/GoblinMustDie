using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManager
{
    private static ResourceManager instance;
    
    private float coin;
    private float wood;
    private float rock;

    private ResourceManager()
    {
        
    }

    public static ResourceManager Instance()
    {
        if (instance != null)
        {
            instance = new ResourceManager();
        }

        return instance;
    }

    public float Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    public float Wood
    {
        get { return wood; }
        set { wood = value; }
    }

    public float Rock
    {
        get { return rock; }
        set { rock = value; }
    }

    public void ChangeCoin(float number)
    {
        coin += number;
    }

    private void ChangeWood(float number)
    {
        wood += number;
    }

    private void ChangeRock(float number)
    {
        rock += number;
    }
}
public enum ResourceType
{
    COIN,
    WOOD,
    ROCK
}