using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
   public static WaypointManager instance;
   
   public List<Transform> goblinWayPoints = new List<Transform>();
   public List<Transform> skeletonWayPoints = new List<Transform>();
   public List<Transform> trollWayPoints = new List<Transform>();
   
   private List<List<Transform>> wayPointLists = new List<List<Transform>>();
   private List<Color> colors = new List<Color>();

   public List<List<Transform>> WayPointLists
   {
      get => wayPointLists;
   }
   
   private void Awake()
   {
      instance = this;
      
      wayPointLists.Add(goblinWayPoints);
      wayPointLists.Add(skeletonWayPoints);
      wayPointLists.Add(trollWayPoints);
      
      colors.Add(Color.blue);
      colors.Add(Color.red);
      colors.Add(Color.green);
   }
   
   private void OnDrawGizmos()
   {
      for (int i = 0; i < wayPointLists.Count; i++)
      {
         DrawPath(wayPointLists[i], colors[i]);
      }
   }

   private void DrawPath(List<Transform> list, Color color)
   {
      foreach (Transform t in list)
      {
         Gizmos.color = color;
         Gizmos.DrawSphere(t.position, 1f);
      }
      
      Gizmos.color = color;
      for (int i = 0; i < list.Count - 1; i++)
      {
         Gizmos.DrawLine(goblinWayPoints[i].position, goblinWayPoints[i + 1].position);
      }
   }
}
