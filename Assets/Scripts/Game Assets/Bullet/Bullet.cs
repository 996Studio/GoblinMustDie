using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Transform target;
    protected float speed = 70f;
    protected float SecondsBeforeDestroy = 2f;
    protected int attack;
    protected GameObject impactEffect;

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
    void Update()
    {
        // If target is no longer available, destroy this bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;   // Constant speed

        // "dir.magnitude" is the current distance to the target
        // Basically means we want our bullet to hit, before move pass this target
        if (dir.magnitude <= distanceThisFrame)
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
