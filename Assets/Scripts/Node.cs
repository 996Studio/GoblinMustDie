using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Range(1,10)]
    public int numberOfRow = 5;
    [Range(1,10)]
    public int numberOfColum = 5;

    [Range(1, 4)] public float sizeOfNodes = 4.0f;
    [Range(1, 4)] public float distancebwNodes = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
