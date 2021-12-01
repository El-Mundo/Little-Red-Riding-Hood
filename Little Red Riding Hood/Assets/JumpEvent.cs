using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpEvent : MonoBehaviour
{
    public bool isJumpIndicator = true;
    public string guideline = "";
    public bool autoInput = false;
    private bool activated = true;
    private static ExplicitController controller;
    private static PlayerBehavior player;
    private static Animator animator;
    private static Text text;

    public static void InitializePlayerPointer(PlayerBehavior _player, ExplicitController _controller, Animator _animator, Text _text)
    {
        player = _player;
        controller = _controller;
        animator = _animator;
        text = _text;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            Time.timeScale = 0.4f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(autoInput) {
            if(col.gameObject.tag == "Player" && activated) {
                activated = false;
                if(isJumpIndicator) {
                    player.PressJump();
                }else {
                    player.PressSlide();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            activated = false;
            Time.timeScale = 0;
            text.text = this.guideline;
            animator.Play("On", 5);
            if(isJumpIndicator) {
                controller.SetState(true);
            }else {
                controller.SetState(false);
            }
        }
    }
}
