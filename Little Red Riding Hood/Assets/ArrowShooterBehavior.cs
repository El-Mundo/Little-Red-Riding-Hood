using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        /*if(col.gameObject.tag == "Player") {
            Debug.Log("WC");
        }*/
        Debug.Log("WC");
    }
}
