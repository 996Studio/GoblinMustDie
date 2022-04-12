using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
   public static WaypointManager instance;
   private void Awake()
   {
      instance = this;
   }

   public List<Transform> wayPoints;

   private void OnDrawGizmos()
   {
      foreach (Transform t in wayPoints)
      {
         Gizmos.color = Color.blue;
         Gizmos.DrawSphere(t.position, 1f);
      }
      
      Gizmos.color = Color.red;
      for (int i = 0; i < wayPoints.Count - 1; i++)
      {
         Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);
      }
   }
}
