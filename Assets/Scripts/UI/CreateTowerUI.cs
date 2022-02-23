using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTowerUI : MonoBehaviour
{
    [SerializeField] private GameObject listPanel;
    // Start is called before the first frame update
    void Start()
    {
        listPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPanel()
    {
        listPanel.SetActive(true);
    }
}
