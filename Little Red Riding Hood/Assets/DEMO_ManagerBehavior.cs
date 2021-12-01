using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEMO_ManagerBehavior : MonoBehaviour
{
    /// <summary>
    /// 游戏局状态：0-运行，1-暂停，2-死亡
    /// </summary>
    private int gameState;
    private int mana;
    private int performScore, distanceScore, score;
    public Text manaDisplay, scoreDisplay;
    private Animator animator;
    public int manaBonus;
    public float distanceBonusFactor;
    private Transform screen;
    private float screenStartX;
    public int fadeFrameCount;
    DEMO_TestCamera camScript;
    Image screenCover;
    public PlayerBehavior player;
    public AudioSource BGM;
    public bool isTutorial = false;

    void Start()
    {
        mana = 0;
        score = performScore = distanceScore = 0;
        gameState = 0;
        animator = GameObject.Find("Canvas").GetComponent<Animator>();
        screen = GameObject.Find("Screen").transform;
        screenStartX = screen.position.x;
        camScript = GameObject.Find("Screen").GetComponent<DEMO_TestCamera>();
        screenCover = GameObject.Find("Screen Cover").GetComponent<Image>();
        //重新分配金币组成就系统静态指针
        TestBonusBehavior.initializeManagers();
        CameraAcceleration.initializeCameraPointer();
        PunishEventBehavior.initializeAchievementPointer();
        DEMO_TeleportScript.InitializePlayerPointer();
        DEMO_LoopBehavior.InitialzieScreenBorder();

        if(!isTutorial) {
            DEMO_TeleportScript.explicitFreeze = false;
            DEMO_TeleportScript.isTutorial = false;
        }else {
            DEMO_TeleportScript.isTutorial = true;
        }
    }

    public int GetGameState()
    {
        return gameState;
    }

    public void Pause()
    {
        if(gameState == 0) {
            gameState = 1;
            animator.Play("GUI Animation");
            animator.Play("Pause On", 2);
            animator.Play("Pause", 4);
            player.UpdateHealthBar();
            Time.timeScale = 0;
            screenCover.color = new Color(0, 0, 0, 0.5f);
            BGM.Pause();
        }else if(gameState == 1) {
            gameState = 0;
            Time.timeScale = 1;
            animator.Play("Play", 4);
            screenCover.color = new Color(0, 0, 0, 0);
            BGM.Play();
        }
    }

    void Update()
    {
        distanceScore = (int) ((screen.position.x - screenStartX) * distanceBonusFactor);
        score = performScore + distanceScore;
        scoreDisplay.text = score.ToString();

        if(Input.GetKeyUp(KeyCode.Escape)) {
            Pause();
        }

        if(gameState == 2) {
            gameState = 3;
            camScript.fadeOut(fadeFrameCount, true);
            CrazyWerewolfController.gameRunning = false;
        }else if(gameState == 3) {
            camScript.fadeOut(fadeFrameCount, false);
        }
    }

    public void addScore(int sc)
    {
        if(gameState >= 2) {
            return;
        }

        performScore += sc;
        animator.Play("GUI Animation");
    }

    public void addMana()
    {
        if(gameState >= 2) {
            return;
        }

        mana += 1;
        manaDisplay.text = mana.ToString();
        addScore(manaBonus);
    }

    public void addLargeMana()
    {
        mana += 10;
        manaDisplay.text = mana.ToString();
        addScore(manaBonus * 20);
    }

    public void gameOver()
    {
        if(gameState >= 2) {
            return;
        }

        gameState = 2;
        animator.Play("Display Results", 3);
    }

    public void ActivatePause()
    {
        animator.Play("Pause On", 2);
    }

    public int GetDistanceScore()
    {
        return distanceScore;
    }

    public int GetPerformScore()
    {
        return performScore;
    }

    public int GetScore()
    {
        return score;
    }

}
