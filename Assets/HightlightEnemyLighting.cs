using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightlightEnemyLighting : MonoBehaviour
{
    private bool activated = true;
    private static Animator lightAnimator;
    public bool hightlight = true;

    public static void InitialziePointer()
    {
        lightAnimator = GameObject.Find("Lighting").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            activated = false;
            if(hightlight) {
                lightAnimator.Play("Highlight");
            }else {
                lightAnimator.Play("Lowlight");
            }
                
        }
    }
}
