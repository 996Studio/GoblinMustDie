using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;
    
    public List<Level> levelList = new List<Level>();

    [Header("Public References")]
    public GameObject levelPointPrefab;
    public GameObject levelButtonPrefab;
    public Transform modelTransform;
    public Transform levelButtonsContainer;
    
    [Header("Tween Settings")]
    public float lookDuration;
    public Ease lookEase;

    public int currentLevelIndex;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Level level in levelList)
        {
            SpawnLevelPoint(level);
        }
        
        if (levelList.Count > 0)
        {
            int maxLevelIndex = 1;
            foreach (Level level in levelList)
            {
                if (level.levelIndex <= GameSetting.instance.maxUnlockedLevelIndex)
                {
                    level.isLocked = false;
                    maxLevelIndex = level.levelIndex;
                }
            }

            currentLevelIndex = maxLevelIndex;
            LookAtLevel(levelList[0]);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(levelButtonsContainer.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLevelPoint(Level l)
    {
        GameObject level = Instantiate(levelPointPrefab, modelTransform);
        level.transform.localEulerAngles = new Vector3(l.y, -l.x, 0.0f);

        SpawnLevelButton(l);
    }
    
    private void SpawnLevelButton(Level l)
    {
        Level level = l;
        Button levelButton = Instantiate(levelButtonPrefab, levelButtonsContainer).GetComponent<Button>();
        levelButton.onClick.AddListener(() => LookAtLevel(level));

        levelButton.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = l.levelName;
        levelButton.GetComponent<LevelSelectButton>().levelIndex = level.levelIndex;
    }

    public void LookAtLevel(Level l)
    {
        Transform cameraParent = Camera.main.transform.parent;
        Transform cameraPivot = cameraParent.parent;

        cameraParent.DOLocalRotate(new Vector3(l.y, 0, 0), lookDuration, RotateMode.Fast).SetEase(lookEase);
        cameraPivot.DOLocalRotate(new Vector3(0,-l.x, 0), lookDuration, RotateMode.Fast).SetEase(lookEase);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelList[currentLevelIndex].levelName);
    }
    
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;

        //only draw if there is at least one stage
        if (levelList.Count > 0)
        {
            for (int i = 0; i < levelList.Count; i++)
            {
                //creat two empty objects
                GameObject point = new GameObject();
                GameObject parent = new GameObject();
                //move the point object to the front of the world sphere
                point.transform.position += - new Vector3(0,0,.5f);
                //parent the point to the "parent" object in the center
                point.transform.parent = parent.transform;

                if (!Application.isPlaying)
                {
                    Gizmos.DrawWireSphere(point.transform.position, 0.02f);
                } 

                //spint the parent object based on the stage coordinates
                parent.transform.eulerAngles += new Vector3(levelList[i].y, -levelList[i].x, 0);
                //draw a gizmo sphere // handle label in the point object's position
                Gizmos.DrawSphere(point.transform.position, 0.07f);
                //destroy all
                DestroyImmediate(point);
                DestroyImmediate(parent);
            }
        }
#endif
    }
}

[System.Serializable]
public class Level
{
    public string levelName;
    public int levelIndex;
    public bool isLocked;

    [Range(-180, 180)] public float x;
    [Range(-89, 89)] public float y;

    [HideInInspector] public Transform visualPoint;
}