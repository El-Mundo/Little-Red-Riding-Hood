using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_LoopBehavior : MonoBehaviour
{
    private static Transform screenBorder;
    private Transform loopPoint;
    public Transform counterPart;
    private Vector3 dif;
    private Transform counterLoop, thisSeal;
    public bool spawnWerewolfOnEnd = false;
    public CrazyWerewolfController werewolf;

    public static void InitialzieScreenBorder()
    {
        screenBorder = GameObject.Find("Background Loop Handler").transform;
    }

    void Start()
    {
        loopPoint = transform.Find("Loop Point");
        thisSeal = transform.Find("Seal Point");
        counterLoop = counterPart.Find("Loop Point");
    }

    void Update()
    {
        if(loopPoint.position.x <= screenBorder.position.x) {
            dif = counterLoop.position - thisSeal.position;
            transform.position += dif;

            if(spawnWerewolfOnEnd) {
                werewolf.Respawn();
            }
        }
    }
}
