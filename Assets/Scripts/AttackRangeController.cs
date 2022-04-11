using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeController : MonoBehaviour
{
    private AttackTower attackTower;
    private float AttackRangeScale = 8;

    private float attackTowerRange;

    private float scale; 
    // Start is called before the first frame update
    void Start()
    {
        attackTower = GetComponentInParent<AttackTower>();
        SetAttackRangeScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetAttackRangeScale()
    {
        attackTowerRange = attackTower.AttackRange;
        Debug.Log(attackTowerRange);
        Vector3 scale = new Vector3(attackTowerRange / AttackRangeScale,1,attackTowerRange / AttackRangeScale);
        transform.localScale = scale;
    }
    
}
