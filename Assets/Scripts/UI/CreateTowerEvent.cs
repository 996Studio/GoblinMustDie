using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateTowerEvent
{
    //public delegate void Action;
    public static event Action menuCall;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            menuCall?.Invoke();
        }
    }
}
