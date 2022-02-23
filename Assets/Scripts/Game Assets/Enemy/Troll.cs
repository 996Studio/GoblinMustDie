using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyBase,IHeal
{
    public void heal()
    {
        if (!isDead)
        {
            curHP += maxHP * 0.2;
        }
    }
}
