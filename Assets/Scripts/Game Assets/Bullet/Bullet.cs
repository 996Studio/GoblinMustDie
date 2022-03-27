using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Transform target;
    [SerializeField]protected float speed;
    [SerializeField]protected float SecondsBeforeDestroy;
    protected int attack;
    [SerializeField]protected GameObject HitEffect;
    protected bool isUsed = false;
    
    [SerializeField] protected ElementEnum elementType;
    [SerializeField] protected float elementAmount;
    [SerializeField] protected int elementPower;

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    protected void Update()
    {
        // If target is no longer available, destroy this bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Debug.Log($"From {transform.position} to {target.position}");
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;   // Constant speed

        // "dir.magnitude" is the current distance to the target
        // Basically means we want our bullet to hit, before move pass this target
        if (dir.magnitude <= distanceThisFrame && !isUsed)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    protected virtual void HitTarget()
    {
        Debug.Log("Base class hit target");
    }
}
