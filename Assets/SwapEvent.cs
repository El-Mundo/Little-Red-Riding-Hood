using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapEvent : MonoBehaviour
{
    private bool activated = true;
    private static Text text;
    public string guideline = "";
    private static Animator animator;

    public static void InitializePointer(Text _text, Animator _animator)
    {
        text = _text;
        animator = _animator;
        DEMO_TeleportScript.explicitVersion = true;
        DEMO_TeleportScript.explicitFreeze = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            Time.timeScale = 0.4f;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            activated = false;
            Time.timeScale = 0;
            text.text = this.guideline;
            animator.Play("Enemy", 5);
            DEMO_TeleportScript.explicitFreeze = false;
        }
    }
}
