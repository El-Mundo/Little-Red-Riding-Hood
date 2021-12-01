using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAcceleration : MonoBehaviour
{
    public float acceleration;
    private bool activated = true;
    private static DEMO_TestCamera cameraScript;

    public static void initializeCameraPointer()
    {
        cameraScript = GameObject.Find("Screen").GetComponent<DEMO_TestCamera>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            cameraScript.addXSpeed(acceleration);
            activated = false;
            //Destroy(gameObject);
            Debug.Log("ACCELERATED");
        }
    }
}
