using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_BossBehavior : MonoBehaviour
{
    public AudioSource biteSound, lowRoarSound, roarSound;
    public AudioSource clawSound1, clawSound2, dogSound;
    public AudioSource damageSound, waterSound;
    public AudioSource bossBGM;

    private int idleTime = 0;
    /// <summary>
    /// 0-未激活，1-步行，2-咬攻，3-爪攻，4-连攻
    /// </summary>
    private int bossState = 0;
    private Animator animator;
    public int MaxHP = 4;
    private int HP = 4, invincible = 0;
    public DEMO_BossDamageBehavior damageHandler;
    public DEMO_AchievementBehavior achievementBehavior;
    public PlayerBehavior player;

    public void Activate()
    {
        idleTime = 0;
        bossState = 1;
        HP = MaxHP;
        animator.Play("Boss Enter");
        invincible = 0;
    }

    public void DebugActivate()
    {
        idleTime = 0;
        bossState = 1;
        HP = MaxHP;
        gameObject.GetComponent<Animator>().Play("Boss Enter");
        invincible = 0;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    void FixedUpdate()
    {
        if(invincible > 0) {
            invincible --;
        }
    }

    public void Hurt()
    {
        if(invincible > 0) {
            return;
        }

        HP --;
        if(HP <= 0) {
            invincible = 99999;
            damageHandler.bossAlive = false;
            animator.Play("Boss Die");
            player.Victory();
            //打败BOSS获得最后一个成就
            achievementBehavior.collectInGroup(5);
        }else {
            animator.Play("Hurt");
        }
        invincible = 60;
    }

    // 在Idle结束调用
    void IdleEnd()
    {
        idleTime ++;
        bossState = 1;
        animator.SetInteger("State", bossState);

        if(idleTime == 1) {
            int i = Random.Range(0, 100);
            if(i > 50) {
                Attack();
            }
        }else if(idleTime == 2) {
            int i = Random.Range(0, 100);
            if(i > 75) {
                Attack();
            }
        }else if(idleTime >= 3) {
            Attack();
        }
    }

    void Attack()
    {
        idleTime = 0;
        int i = Random.Range(0, 100);
        if(i < 45) {
            bossState = 2;
        }else if(i < 90) {
            bossState = 3;
        }else {
            bossState = 4;
        }

        animator.SetInteger("State", bossState);
        bossState = 1;
    }

    void PlayBiteSound()
    {
        biteSound.Play();
    }

    void PlayLowRoarSound()
    {
        lowRoarSound.Play();
    }

    void PlayClawSound1()
    {
        clawSound1.Play();
    }

    void PlayClawSound2()
    {
        clawSound2.Play();
    }

    void PlayRoarSound()
    {
        roarSound.Play();
    }

    void PlayBossBGM()
    {
        bossBGM.Play();
    }

    void PlayDogSound()
    {
        dogSound.Play();
    }

    void PlayDamageSound()
    {
        damageSound.Play();
    }

    void PlayWaterSound()
    {
        waterSound.Play();
    }
}
