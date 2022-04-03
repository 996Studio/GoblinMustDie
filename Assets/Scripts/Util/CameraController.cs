////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: CameraController.cs
//Author: Zihan Xu
//Last Modified On : 1/19/2022
//Description : Class for camera
//Revision History:
//1/19/2022: Implement feature of camera moving and zooming
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("PC")]
    public float panSpeed;
    public float panBorderThickness;
    public float scrollSpeed;
    public float minYPos;
    public float maxYPos;
    public Vector2 minPos;
    public Vector2 maxPos;

    [Header("Mobile")]
    public float zoomOutMin;
    public float zoomOutMax;
    public Camera cam;
    public float groundZ = 0;
    private Vector3 touchStart;
    // Update is called once per frame
    void Update()
    {
        
        #if  UNITY_STANDALONE || UNITY_WEBGL
        //Move camera
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        //Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            Vector3 pos = transform.position;
            pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minYPos, maxYPos);
            transform.position = pos;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), transform.position.y,
            Mathf.Clamp(transform.position.z, minPos.y, maxPos.y));
#endif

        #if UNITY_EDITOR || UNITY_IOS  || UNITY_ANDROID 
        if (Input.GetMouseButtonDown(0))
        {
            //touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchStart = GetWorldPosition(groundZ);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference);
        }
        
       
        else if (Input.GetMouseButton(0))
        {
            //Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = touchStart - GetWorldPosition(groundZ);
            Camera.main.transform.position += direction;
        }
        #endif
        
       
    }
    private void zoom(float increment)
    {
        Vector3 pos = transform.position;
        pos.y -= increment * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minYPos, maxYPos);
        transform.position = pos;
       
    }

    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}
