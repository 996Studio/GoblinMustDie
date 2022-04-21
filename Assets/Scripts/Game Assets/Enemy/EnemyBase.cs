using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Enum;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum EnemyType
{
    GOBLIN,
    SKELETON,
    TROLL,
    NONE
}

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected NavMeshAgent agent;
    protected Rigidbody rigidbody;
    
    protected float maxHP;
    protected float curHP;
    protected int atk;
    protected float moveSpeed;
    protected int coinValue;
    
    [SerializeField] 
    private Image HPBarForeground;

    private float updateSpeedSec = 0.3f;
    protected bool isDead = false;
    protected bool canTakeDamage = true;
    private float recycleMultiplier;
    protected ElementComponent elementComponent;
    
    //Freeze Parameter
    private float freezeCounter;

    [Header("Hit Effect")] 
    public ParticleSystem overloadeffect;
    public ParticleSystem electricityeffect;
    public ParticleSystem freezeeffect;
    public ParticleSystem steameffect;
    private HitEffect hitEffect;

    //Waypoint Pathfinding Params
    protected List<Transform> wayPoints;
    private int destinationIndex = 0;
    protected EnemyType enemyType;
    
    public float RecycleMultiplier
    {
        get => recycleMultiplier;
        set => recycleMultiplier = value;
    }
    
    public event Action<float> OnHealthChanged = delegate(float f) {  };
    
    public int Atk
    {
        get => atk;
    }
    
    public bool IsDead
    {
        get => isDead;
    }
    
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        canTakeDamage = true;
        
        //delegate for changing health
        OnHealthChanged += HandleHealthChange;
        HPBarForeground.fillAmount = 0.5f;
        
        elementComponent = GetComponent<ElementComponent>();
    }
    
    protected void Start()
    {
        SetPath();
        InitInfo();
    }
    
    protected virtual void Update()
    {
        changeWayPointIndex();
        
        FreezeTimer();
        
        //Test code
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            elementComponent.ElementAttack(ElementEnum.Fire, 1.0f, 1, 5);
            //Debug.Log("Fire attack");
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            elementComponent.ElementAttack(ElementEnum.Water, 10.0f, 1, 5);
            //Debug.Log("Water attack");
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            elementComponent.ElementAttack(ElementEnum.Ice, 1.0f, 1, 5);
            //Debug.Log("Ice attack");
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            elementComponent.ElementAttack(ElementEnum.Thunder, 10.0f, 1, 5);
            //Debug.Log("Thunder attack");
        }
    }
    
    public virtual void InitInfo()
    {
        atk = 1;
    }

    protected void SetPath()
    {
        switch (enemyType)
        {
            case EnemyType.GOBLIN:
                wayPoints = WaypointManager.instance.goblinWayPoints;
                break;
            case EnemyType.SKELETON:
                wayPoints = WaypointManager.instance.skeletonWayPoints;
                break;
            case EnemyType.TROLL:
                wayPoints = WaypointManager.instance.trollWayPoints;
                break;
            default:
                break;
        }

        agent.SetDestination(wayPoints[destinationIndex].position);
    }

    protected void changeWayPointIndex()
    {
        if (Vector3.Distance(agent.transform.position, wayPoints[destinationIndex].position) <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[++destinationIndex].position);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            curHP -= dmg;
            
            if (curHP <= 0)
            {
                animator.SetTrigger("Death");
                SetSpeed();
            }
            else
            {
                animator.SetTrigger("Hurt");
                SetSpeed();
            }
            
            //Debug.Log($"damage {dmg} to {curHP}");
            //ChangeHealth();
        }
    }
    
    protected virtual void AnimParaReset()
    {
        //Debug.Log("Anim Para Reset!");
        canTakeDamage = true;
        agent.isStopped = false;
        agent.speed = moveSpeed;
    }
    
    protected void SetSpeed()
    {
        agent.isStopped = true;
    }

    public void ElementAttack(ElementEnum element, float amount, int power, int damage)
    {
        elementComponent.ElementAttack(element, amount, power, damage);
    }

    public void ChangeHealth()
    {
        float curHealthPct = curHP / maxHP;
        OnHealthChanged(curHealthPct);
    }

    public virtual void Death()
    {
        Debug.Log("Death Called!");
        
        isDead = true;
        
        AudioManager.instance.Play(SoundType.SFX, "EnemyDeath");

        GameManager.Instance.KillCount++;
        GameManager.Instance.SpawnNum--;
        
        ResourceManager.Instance().ChangeCoin(coinValue);
        HUDManager.instance.UpdateCoinText(ResourceManager.Instance().Coin);
        
        Destroy(this.gameObject);
    }

    public void MotherbaseDestroy()
    {
        AudioManager.instance.Play(SoundType.SFX, "EnemyDeath");

        GameManager.Instance.KillCount++;
        GameManager.Instance.SpawnNum--;
        
        ResourceManager.Instance().ChangeCoin(coinValue);
        HUDManager.instance.UpdateCoinText(ResourceManager.Instance().Coin);
        
        Destroy(this.gameObject);
    }

    private void HandleHealthChange(float pct)
    {
        StartCoroutine(ChangeHPBarPct(pct));
    }

    private IEnumerator ChangeHPBarPct(float pct)
    {
        float preChangePct = HPBarForeground.fillAmount;
        float timeElapsed = 0.0f;
        canTakeDamage = false;

        while (timeElapsed < updateSpeedSec)
        {
            timeElapsed += Time.deltaTime;
            HPBarForeground.fillAmount = Mathf.Lerp(preChangePct, pct, timeElapsed / updateSpeedSec);
            
            yield return null;
        }

        canTakeDamage = true;
        HPBarForeground.fillAmount = pct;
    }

    private void FreezeTimer()
    {
        if (freezeCounter > 0.0f)
        {
            freezeCounter -= Time.deltaTime;
            if (freezeCounter <= 0)
            {
                freezeCounter = 0.0f;
                StopSlowDown();
            }
        }
    }

    public void StartSlowDown(float freezeFactor, float duration)
    {
        agent.speed = freezeFactor;
        freezeCounter = duration;
    }

    public void StopSlowDown()
    {
        agent.speed = moveSpeed;
    }
    
    public void Overload(int damage)
    {
        TakeDamage(damage);
        //rigidbody.AddForce(Vector3.back * 100.0f);
        PlayHitEffect(HitEffect.Overload);
    }

    public void ElectroCharged(int damage)
    {
        //Debug.Log("Electro Charge");
        TakeDamage(damage);
        PlayHitEffect(HitEffect.Electro);
    }

    public void Freeze()
    {
        //Debug.Log("Start freezing");
        agent.speed = 0;
    }

    public void Unfreeze()
    {
        Debug.Log("Stop freezing");
        agent.speed = moveSpeed;
    }

    public List<EnemyBase> GetNearestEnemy(int number, float range)
    {
        List<EnemyBase> targetList = new List<EnemyBase>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        List<EnemyBase> tempEnemyList = new List<EnemyBase>();
        foreach (var collider in colliders)
        {
            EnemyBase tempEnemy = collider.GetComponent<EnemyBase>();
            if (tempEnemy != this && tempEnemy != null)
            {
                Debug.Log(tempEnemy);
                tempEnemyList.Add(tempEnemy);
            }
        }
        
        //Debug.Log($"unordered {tempEnemyList.Count}");
        if (tempEnemyList.Count == 0)
        {
            return targetList;
        }
        
        var orderedEnemies = tempEnemyList.OrderBy(e => Vector3.Magnitude(e.transform.position - transform.position))
            .ToList();
        //Debug.Log($"ordered {orderedEnemies.Count()}");

        foreach (var nearbyEnemy in orderedEnemies)
        {
            targetList.Add(nearbyEnemy);
            if (targetList.Count >= number)
            {
                return targetList;
            }
        }
        
        return targetList;
    }

    public void PlayHitEffect(HitEffect effect)
    {
        switch(effect) 
        {
            case HitEffect.Electro:
                electricityeffect.Play();
               // Debug.Log("Electro!");
                break;
            case HitEffect.Overload:
                overloadeffect.Play();
                //Debug.Log("Overload!");
                break;
            case HitEffect.Freeze:
                freezeeffect.Play();
                //Debug.Log("Freeze!");
                break;
            case HitEffect.Vaporize:
                steameffect.Play();
                //Debug.Log("Vaporize!");
                break;
            default:
                break;
        }
    }
    
    public void StopHitEffect(HitEffect effect)
    {
        switch(effect) 
        {
            case HitEffect.Electro:
                electricityeffect.Stop();
                break;
            case HitEffect.Overload:
                overloadeffect.Stop();
                break;
            case HitEffect.Freeze:
                freezeeffect.Stop();
                break;
            case HitEffect.Vaporize:
                steameffect.Stop();
                break;
            default:
                break;
        }
    }
}


public enum HitEffect
{
    Electro = 1,
    Overload = 2,
    Freeze = 3,
    Vaporize = 4
}