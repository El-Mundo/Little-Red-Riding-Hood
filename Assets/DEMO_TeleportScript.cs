using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_TeleportScript : MonoBehaviour
{
    private static Transform player, playerTeleport;
    private Transform thisTeleport;
    private int freeze;
    public int freezeTime = 255;
    private ParticleSystem thisParticle;
    private static ParticleSystem playerParticle;
    private static AudioSource teleportSound;
    private static DEMO_ManagerBehavior gameManager;
    private static AudioSource fallenSound;
    private static DEMO_AchievementBehavior achievementBehavior;
    public static bool explicitVersion = false, explicitFreeze = true, isTutorial = false;

    public static void InitializePlayerPointer()
    {
        player = GameObject.Find("Player").transform;
        playerTeleport = player.Find("teleport point");
        playerParticle = GameObject.Find("Player Particle").GetComponent<ParticleSystem>();
        teleportSound = GameObject.Find("Teleport Player").GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<DEMO_ManagerBehavior>();
        fallenSound = GameObject.Find("Fallen Sound").GetComponent<AudioSource>();
        achievementBehavior =  GameObject.Find("Achievement Manager").GetComponent<DEMO_AchievementBehavior>();
    }

    void Start()
    {
        freeze = 0;
        thisTeleport = transform.Find("Teleport Point");
        thisParticle = transform.Find("Particle System").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if(hitInfo) {
                    Debug.Log( hitInfo.transform.gameObject.name );
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(freeze > 0) {
            freeze --;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Screen Border") {
            gameManager.addScore(500);
            fallenSound.Play();
            //击败任何敌人可以获得4号成就
            achievementBehavior.collectInGroup(4);
            Debug.Log("ENEMY DEFEATED!");
        }
    }

    void OnMouseDrag()
    {
        Debug.Log("TOUCH " + gameObject.name);

        //传送冷却中，中止位移
        if(freeze > 0 || gameManager.GetGameState() != 0 || (explicitVersion && explicitFreeze)) {
            return;
        }

        //传送开始，准备冷却
        freeze = freezeTime;

        //计算传送点坐标差
        Vector3 playerPos = player.position;
        Vector3 playerTelePos = playerTeleport.position;
        Vector3 thisPos = gameObject.transform.position;
        Vector3 thisTelePos = thisTeleport.position;

        //坐标差使二者交换位置
        Vector3 move = thisTelePos - playerTelePos;
        player.position += move;
        transform.position -= move;

        //播放粒子特效和音效
        thisParticle.Play();
        playerParticle.Play();
        teleportSound.Play();

        if(explicitVersion && isTutorial) {
            GameObject.Find("显性中控").GetComponent<ExplicitController>().Swap();
            explicitFreeze = true;
        }
    }

}
