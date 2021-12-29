using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_BossDamageBehavior : MonoBehaviour
{
    public DEMO_BossBehavior boss;
    public bool bossAlive = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Chainball" && bossAlive) {
            boss.Hurt();
        }
    }
}
