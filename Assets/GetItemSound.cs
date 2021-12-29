using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemSound : MonoBehaviour
{
    private AudioSource sound;
    public Animator HUDAnimator;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void PlayItemSound()
    {
        sound.Play();
    }

    void PlayHudAnimation()
    {
        HUDAnimator.Play("HUD Item");
    }
}
