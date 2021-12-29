using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public static AudioSource ArrowSound;
    private static AudioClip ArrowWarning, ArrowShoot;
    private static EnemyManagerBehavior manager;
    private static Transform screenPosition, scenePosition, screenTrigger;
    private Animator animator;
    private Transform spawnPoint;
    private bool spawned;

    public static void InitializeArrowAudio(AudioClip warning, AudioClip shoot, EnemyManagerBehavior managerBehavior)
    {
        ArrowWarning = warning;
        ArrowShoot = shoot;
        manager = managerBehavior;
        screenPosition = GameObject.Find("Screen Enemies").transform;
        scenePosition = GameObject.Find("Scene Enemies").transform;
        screenTrigger = GameObject.Find("Screen Border Right").transform;
        ArrowSound = GameObject.Find("Arrow Audio Player").GetComponent<AudioSource>();
    }

    void Start()
    {
        spawnPoint = transform.Find("Spawn Point");
        animator = gameObject.GetComponent<Animator>();
        spawned = false;
    }

    void Update()
    {
        if(screenTrigger.position.x >= spawnPoint.position.x && !spawned) {
            SpawnInCamera();
        }
    }

    void PlayWarningSound()
    {
        ArrowSound.clip = ArrowWarning;
        ArrowSound.Play();
    }

    void PlayShootSound()
    {
        ArrowSound.clip = ArrowShoot;
        ArrowSound.Play();
    }

    void SpawnInCamera()
    {
        transform.SetParent(screenPosition);
        animator.Play("Arrow Animation");
        spawned = true;
    }

    void Inactivate()
    {
        transform.SetParent(scenePosition);
        transform.position = manager.transform.position;
    }

    public static Transform GetScreenTrigger()
    {
        return screenTrigger;
    }
}
