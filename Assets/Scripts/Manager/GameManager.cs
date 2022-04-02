using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int killCount;

    private int curHP;
    private int maxHP;

    private bool isDead = false;
    private bool isWin = false;
    
    private bool isPaused;
    
    public PauseMenu PauseMenu;
    private ResourceManager resourceManager;

    public static GameManager Instance;
    public GameObject minimap;
    
    private bool isSpawnEnd;
    private int spawnNum;
    
    //public List<GameObject> spawnedEnemies;

    public Animator pauseAnim;
    public GameObject GameOverScreen;

    public int CurHp
    {
        get => curHP;
        set => curHP = value;
    }
    
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    public bool IsWin
    {
        get => isWin;
        set => isWin = value;
    }
    
    public int KillCount
    {
        get => killCount;
        set => killCount = value;
    }
    
    public bool IsSpawnEnd
    {
        get => isSpawnEnd;
        set => isSpawnEnd = value;
    }
    
    public int SpawnNum
    {
        get => spawnNum;
        set => spawnNum = value;
    }
    
    void Awake()
    {
        Instance = this;
        curHP = 10;
        maxHP = 10;
        isSpawnEnd = false;
        spawnNum = 0;
    }

    private void Start()
    {
        HUDManager.instance.UpdateLiveText(this.curHP);
        AudioManager.instance.Play(SoundType.MUSIC,"BGM");
    }

    private void Update()
    {
        if (isSpawnEnd & spawnNum == 0 && !isDead)
        {
            isWin = true;
        }

        if (isDead)
        {
            GameObject.Find("LevelData").GetComponent<LevelData>().isWin = false;
            GameObject.Find("LevelData").GetComponent<LevelData>().killCount = killCount * 10000;
            Debug.Log(
                $"Mother Win {GameObject.Find("LevelData").GetComponent<LevelData>().isWin}, kill {GameObject.Find("LevelData").GetComponent<LevelData>().killCount}");
            //GameOverScreen.SetActive(true);
            //Time.timeScale = 0f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            SceneManager.LoadScene("WinScene");
        }
        else if (isWin)
        {
            GameObject.Find("LevelData").GetComponent<LevelData>().isWin = true;
            GameObject.Find("LevelData").GetComponent<LevelData>().killCount = killCount * 10000;
            
            //GameOverScreen.SetActive(true);
            //Time.timeScale = 0f;

            SceneManager.LoadScene("WinScene");
        }
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        PauseMenu.ShowPauseMenu();
        pauseAnim.SetBool("onShowPause", true);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseAnim.SetBool("onShowPause", false);
        PauseMenu.HidePauseMenu();
    }
    
    public void TakeDamage(int dmg)
    {
        if (isDead)
            return;
        
        curHP -= dmg;

        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
        }

        UpdateHp(curHP, maxHP);
    }
    public void UpdateHp(int hp, int maxHp)
    {
        this.curHP = hp;
        this.maxHP = maxHp;
        
        //Update UI info later
        HUDManager.instance.UpdateLiveText(this.curHP);
    }

    public void ToggleMinimap()
    {
        minimap.SetActive(!minimap.activeInHierarchy);
    }

}
