using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public GameObject touchedObject;

    public Node touchedNode;

    public static TouchManager instance;

    public GameObject debugCube;

    private void Awake()
    {
        if (instance != null)
        {
            print("Hello???");
        }

        instance = this;
    }

    Ray GenerateMouseRay(Vector3 touchPos)
    {
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);

        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray mouseRay = GenerateMouseRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
                {
                    // GUI Action
                    print("Raycast on UI");
                    return;
                }
                else
                {
                    /*  Old Botton UI Panel  */
                    //                     if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                    //                     {
                    //                         print("Raycast on GameObject(collider): " + hit.collider); 
                    //                         touchedObject = hit.transform.gameObject;
                    // 
                    //                         if (touchedObject.CompareTag("Node"))
                    //                         {
                    //                             touchedNode = touchedObject.GetComponent<Node>();
                    //                             CreateTowerUI.instance.selectNode = touchedNode;
                    //                             CreateTowerUI.instance.lastNode = touchedNode;
                    //                             CreateTowerUI.instance.showPanel();
                    //                         }
                    //                     }

                    // New Circle UI
                    if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                    {
                        print("Raycast on GameObject(collider): " + hit.collider);
                        touchedObject = hit.transform.gameObject;

                        if (touchedObject.CompareTag("Node"))
                        {
                            touchedNode = touchedObject.GetComponent<Node>();
                            CreateTowerUI.instance.selectNode = touchedNode;
                            CreateTowerUI.instance.lastNode = touchedNode;

                            FollowMenu.instance.SelectedNode = touchedNode;
                            FollowMenu.instance.lastNode = touchedNode;

                            CreateTowerUI.instance.showPanel();
                        }
                        else
                        {
                            FollowMenu.instance.HideMenu();
                        }

                    }
                }
                
            }
        }
    }
}
