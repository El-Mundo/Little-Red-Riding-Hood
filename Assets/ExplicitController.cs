using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplicitController : MonoBehaviour
{
    private bool jumping = true;
    private PlayerBehavior player;
    public Animator animator;
    public Text text;
    private float freeze;
    public float freezeTime = 1.0f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        JumpEvent.InitializePlayerPointer(player, this, animator, text);
        SwapEvent.InitializePointer(text, animator);
        HightlightEnemyLighting.InitialziePointer();
        freeze = freezeTime;
    }

    void Update()
    {
        if(freeze > 0) {
            freeze -= Time.unscaledDeltaTime;
        }
    }

    public void PushExplicitJump()
    {
        if(freeze > 0.1f) {
            return;
        }

        if(jumping) {
            Time.timeScale = 1;
            player.PressJump();
            animator.Play("Hidden", 5);
        }
    }

    public void Swap()
    {
        if(jumping) {
            Time.timeScale = 1;
            animator.Play("Hidden", 5);
        }
    }

    public void PushExplicitSlide()
    {
        if(freeze > 0.1f) {
            return;
        }

        if(!jumping) {
            Time.timeScale = 1;
            player.PressSlide();
            animator.Play("Hidden", 5);
        }
    }

    public void SetState(bool jumpingNotSliding)
    {
        freeze = freezeTime;
        jumping = jumpingNotSliding;
        animator.SetBool("Jump", jumping);
    }
}
