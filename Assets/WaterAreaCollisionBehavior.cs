using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAreaCollisionBehavior : MonoBehaviour
{
    private AudioSource sound;
    private Transform screen;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        screen = GameObject.Find("Screen").transform;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && screen.position.x > 480.0f) {
            sound.Play();
        }
    }
}
