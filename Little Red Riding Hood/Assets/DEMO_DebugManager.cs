using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_DebugManager : MonoBehaviour
{
    public bool DEBUG = false;
    public float TimeScale = 1.0f;
    public float ScreenStartX = 12.11246f;
    public float cameraStartSpeed = 0.1f;
    public bool bossFight = false;

    void Start()
    {
        if(DEBUG) {
            GameObject.Find("Screen").transform.position = new Vector3(ScreenStartX, 5.719013f, -11.15094f);
            GameObject.Find("Screen").GetComponent<DEMO_TestCamera>().cameraSpeed = cameraStartSpeed;
            if(bossFight) {
                GameObject.Find("BOSS").GetComponent<DEMO_BossBehavior>().DebugActivate();
            }
        }
    }

    void FixedUpdate()
    {
        if(DEBUG) {
            Time.timeScale = TimeScale;
        }
    }
}
