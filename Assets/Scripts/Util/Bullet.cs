using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;


    public float speed = 70f;

    public float SecondsBeforeDestroy = 2f;

    public GameObject impactEffect;

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

    void HitTarget()
    {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        if (target != null)
        {
            Destroy(gameObject, SecondsBeforeDestroy);
        }
        else
        {
            Destroy(gameObject);
        }

        Destroy(target.gameObject);
        //StartCoroutine(WaitBeforeDestroy());
    }

    //IEnumerator WaitBeforeDestroy()
    //{
    //    yield return new WaitForSeconds(bulletDestroyTimer);
    //    Destroy(gameObject);
    //}
}
