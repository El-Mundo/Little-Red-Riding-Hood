using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightEntrance : MonoBehaviour
{
    private bool activated = true;
    private PlayerBehavior player;
    private DEMO_BossBehavior boss;
    public Animator bgm;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        boss = GameObject.Find("BOSS").GetComponent<DEMO_BossBehavior>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            player.EnterBossFight();
            boss.Activate();
            bgm.Play("Music Fade Out");
            activated = false;
        }
    }
}
