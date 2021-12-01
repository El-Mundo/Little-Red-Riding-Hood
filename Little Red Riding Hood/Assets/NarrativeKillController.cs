using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeKillController : MonoBehaviour
{
    public AudioSource whipSound, fallenSound;
    private Animator animator;
    public Animator itemAnimator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void PlayWhipSound()
    {
        whipSound.Play();
    }

    public void PlayFallenSound()
    {
        fallenSound.Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            animator.Play("Narrative");
        }
    }

    void PlayItemAnimation()
    {
        itemAnimator.Play("Item Fly to Player");
    }
}
