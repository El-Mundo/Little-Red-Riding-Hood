using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_TestCamera : MonoBehaviour
{
    public float cameraSpeed = 2.0f;
    private Vector3 v;
    private Vector3 fadeVec;

    void Start()
    {
        v = new Vector3(cameraSpeed, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        gameObject.transform.position += v;
    }

    public void setXSpeed(float speed)
    {
        v = new Vector3(speed, v.y, 0);
    }

    public void addXSpeed(float speed)
    {
        cameraSpeed += speed;
        v = new Vector3(cameraSpeed, v.y, 0);
    }

    public void fadeOut(int frameCount, bool isFirstCall)
    {
        if(isFirstCall) {
            v = new Vector3(v.x, 0, 0);
            fadeVec = new Vector3(v.x / frameCount, 0, 0);
        }

        if(v.x > fadeVec.x) {
            v -= fadeVec;
        }
    }

}
