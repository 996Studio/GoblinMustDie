using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    protected int hp;
    protected int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
