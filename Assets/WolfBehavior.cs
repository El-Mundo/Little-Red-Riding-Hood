using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehavior : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;
    private bool falling;
    private static Transform screenTrigger;
    private bool spawned;
    private Transform spawnPoint;
    public AudioSource wolfSound;

    public static void InitializePointer(AudioClip fallenAudio)
    {
        screenTrigger = ArrowBehavior.GetScreenTrigger();
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spawnPoint = transform.Find("Spawn Point");
        falling = false;
        spawned = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Border") {
            Inactivate();
        }
    }

    void SpawnInCamera()
    {
        animator.SetBool("Visible", true);
        Debug.Log(gameObject.name + " ENTERED");
        spawned = true;
    }

    void Update()
    {
        if(screenTrigger.position.x >= spawnPoint.position.x && !spawned) {
            SpawnInCamera();
        }

        if(rigid.velocity.y < -0.01f) {
            animator.Play("Wolf Fall");
            falling = true;
        }else if(falling) {
            animator.Play("Wolf Idle");
        }
    }

    void PlayWolfSound()
    {
        animator.SetBool("Swaggerred", true);
        wolfSound.Play();
    }

    void Inactivate()
    {
        rigid.simulated = false;
        animator.SetBool("Swaggerred", true);
        animator.Play("Wolf Idle");
    }
}
