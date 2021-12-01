using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSuccessBehavior : MonoBehaviour
{
    public Animator musicAnimator, transitor;
    private bool activated = true;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            activated = false;
            transitor.Play("Tutorial Out");
            musicAnimator.Play("Music Fade Out");
        }
    }
}
