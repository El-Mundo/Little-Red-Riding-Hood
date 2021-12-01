using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyWerewolfController : MonoBehaviour
{
    private static AudioSource woodSound;
    private ParticleSystem particle;
    private Transform spawnPoint;
    private static Transform screenTrigger;
    private bool spawned;
    private Animator animator;
    public bool respawnable = false;
    private bool animated = false;
    private Vector3 compStartPos;
    private Transform compPos;
    public static bool gameRunning;

    public static void InitializeBridgeSoundPointer()
    {
        woodSound = GameObject.Find("Bridge Sound").GetComponent<AudioSource>();
        screenTrigger = ArrowBehavior.GetScreenTrigger();
    }

    void Start()
    {
        particle = transform.Find("Terrain Component").Find("Particle System").GetComponent<ParticleSystem>();
        spawnPoint = transform.Find("Spawn Point");
        animator = gameObject.GetComponent<Animator>();
        spawned = false;
        animated = false;
        compPos = transform.Find("Enemy Controller").Find("Enemy Component");
        compStartPos = compPos.localPosition;
        gameRunning = true;
    }

    void SpawnInCamera()
    {
        animator.SetBool("Visible", true);
        Debug.Log(gameObject.name + " ENTERED");
        spawned = true;
    }

    public void Respawn()
    {
        animator.SetBool("Visible", false);
        spawned = false;
        animator.Play("Werewolf Stay");
        compPos.localPosition = compStartPos;
    }

    void Update()
    {
        if(screenTrigger.position.x >= spawnPoint.position.x && !spawned) {
            SpawnInCamera();
            if(respawnable && animated) {
                Respawn();
                animated = false;
            }
        }
        animator.SetBool("Game Running", gameRunning);
    }

    void PlayWoodSound()
    {
        woodSound.Play();
        particle.Play();
        animated = true;
    }
}
