using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    /// <summary>
    /// 跳跃受力
    /// </summary>
    public float jumpForce = 0.2f;
    /// <summary>
    /// 二段跳受力
    /// </summary>
    public float doubleJumpForce = 0.1f;
    private Vector2 jumpVec, doubleJumpVec;
    /// <summary>
    /// 角色底部是否与地面相交
    /// </summary>
    private bool landed;

    /// <summary>
    /// 玩家滑行总时长
    /// </summary>
    public float slideTime = 1.0f;
    /// <summary>
    /// 玩家滑行速度
    /// </summary>
    public float slideSpeed = 0.005f;
    private Vector2 slideVec;

    /// <summary>
    /// 玩家快速下降速度
    /// </summary>
    public float poundSpeed = -1.0f;
    private Vector2 poundVec;

    Rigidbody2D rigid;
    Animator animator;

    //游戏相机脚本
    DEMO_TestCamera camScript;

    /// <summary>
    /// 角色总状态，0默认跑动，1跳跃上升，2下落，3快速下落(下压)，4滑行, 5二段跳, 6失败
    /// </summary>
    [Tooltip("States: 0-Run, 1-Jumping, 2-Falling, 3-Pound-falling, 4-Sliding, 5-Double jump, 6-Retired")]
    public int playerState = 0;

    /// <summary>
    /// 是否已使用二段跳
    /// </summary>
    private bool hasJumpedTwice = false;

    /// <summary>
    /// 控制状态切替的计时
    /// </summary>
    private float stateTimer = 0.0f;
    /// <summary>
    /// 玩家超出屏幕规定范围时，X轴归位的速度(刚体)
    /// </summary>
    public float returnSpeed = 0.5f;

    /// <summary>
    /// 玩家的HP，最大为3，可以回复
    /// </summary>
    private int health;
    /// <summary>
    /// 玩家的剩余无敌时间
    /// </summary>
    private int invincibleTime;
    [Tooltip("受伤后获得的无敌时间，单位1/60秒")]
    public int defaultInvincibleTimsc;

    private DEMO_ManagerBehavior managerBehavior;
    private Transform leftNormX, rightNormX;
    private Transform leftBossNormX, rightBossNormX;
    private Vector2 returnVector;
    //private bool isHomed;
    private Animator healthDisplay;

    //虚拟键盘输入
    private bool jumpPressed, slidePressed;

    /// <summary>
    /// 踩在木板上还是草地上
    /// </summary>
    private bool steppingOnWood = false;
    public AudioSource woodSound, grassSound;
    public AudioSource coinSound;
    public AudioSource jumpSound, screamSound;
    public AudioClip FirstJumpAudio, DoubleJumpAudio, slideAudio, poundAudio;
    private bool bossFighting = false;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        camScript = GameObject.Find("Screen").GetComponent<DEMO_TestCamera>();
        managerBehavior = GameObject.Find("Game Manager").GetComponent<DEMO_ManagerBehavior>();
        leftNormX = GameObject.Find("Player Norm Left").transform;
        rightNormX = GameObject.Find("Player Norm Right").transform;
        leftBossNormX = GameObject.Find("Player-Boss Norm Left").transform;
        rightBossNormX = GameObject.Find("Player-Boss Norm Right").transform;
        healthDisplay = GameObject.Find("Canvas").GetComponent<Animator>();

        jumpVec = new Vector2(0, jumpForce);
        doubleJumpVec = new Vector2(0, doubleJumpForce);
        slideVec = new Vector2(slideSpeed, 0);
        poundVec = new Vector2(0, poundSpeed);
        returnVector = new Vector2(returnSpeed, 0);
        playerState = 0;
        stateTimer = 0.0f;
        landed = false;
        //isHomed = true;
        health = 3;
        invincibleTime = defaultInvincibleTimsc;
        bossFighting = false;
    }

    void Update()
    {
        animator.SetInteger("Player State", playerState);

        switch (playerState)
        {
            case 0:
                RunningUpdate();
                break;
            case 1:
                JumpingUpdate();
                break;
            case 2:
                FallingUpdate();
                break;
            case 3:
                PoundingUpdate();
                break;
            case 4:
                SlideUpdate();
                break;
            case 5:
                DoubleJumpUpdate();
                break;
            default:
                break;
        }

        if(playerState != 6) {
            checkScreenRelativePosition();
        }
    }

    void FixedUpdate()
    {
        if(invincibleTime > 0) {
            invincibleTime --;
        }
    }

    /// <summary>
    /// 与奖励或敌人相交
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        //如果主角已经失败，不要触发碰撞事件
        if(playerState == 6) {
            return;
        }

        GameObject obj = col.gameObject;
        switch (obj.tag)
        {
            case "Coin":
                obj.GetComponent<TestBonusBehavior>().PlayerCollect();
                coinSound.Play();
                break;
            case "Border":
                //屏幕边界,游戏结束
                managerBehavior.gameOver();
                playerState = 6;
                break;
            case "Enemy":
                this.getHurt();
                break;
            default:
                break;
        }
        /*if(obj.tag == "Coin") {
            obj.GetComponent<TestBonusBehavior>().PlayerCollect();
        }else if (col.gameObject.tag == "Border") { //屏幕边界
            // 游戏结束
            managerBehavior.gameOver();
            //GameMgr.instance.state = GameState.End;
            //PlayerPrefs.DeleteAll();
        }*/

        /*if ("Coin" == col.gameObject.tag) {
            // 吃到金币
            Destroy(col.gameObject);
            GameMgr.instance.score++;
            PlayerPrefs.SetInt("coins", GameMgr.instance.score);
        }*/
        
        /*if(col.gameObject.layer == 10) {
            //GameMgr.instance.state = GameState.End;
            //PlayerPrefs.DeleteAll();
        }*/
    }

    /// <summary>
    /// 与奖励或敌人分离
    /// </summary>
    void OnTriggerExit2D(Collider2D col)
    {
        
    }

    void Jump()
    {
        rigid.velocity = jumpVec;
        playerState = 1;
        jumpSound.clip = FirstJumpAudio;
        jumpSound.Play();
    }
    
    /// <summary>
    /// 跑动状态的更新
    /// </summary>
    void RunningUpdate()
    {
        //重置状态切替计时
        stateTimer = 0.0f;
        //回到默认速度
        camScript.setXSpeed(camScript.cameraSpeed);

        if(Input.GetButtonDown("Up") || CheckJumpVirtualInput()) {
            Jump();
        }else if(Input.GetButtonDown("Down") || CheckSlideVirtualInput()) {
            camScript.setXSpeed(slideVec.x + camScript.cameraSpeed);
            jumpSound.clip = slideAudio;
            jumpSound.Play();
            playerState = 4;
        }

        if(rigid.velocity.y < -0.01f) {
            playerState = 2;
        }

        if(landed == true) {
            hasJumpedTwice = false;
        }
    }

    void Pound()
    {
        playerState = 3;
        rigid.velocity = poundVec;
        jumpSound.clip = poundAudio;
        jumpSound.Play();
    }

    void JumpingUpdate()
    {
        stateTimer += Time.deltaTime;

        CheckDoubleJump();

        if(rigid.velocity.y < 0.01f) {
            playerState = 2;
        }

        if(stateTimer >= 0.2f && landed) {
            playerState = 0;
            return;
        }

        if(Input.GetButtonDown("Down") || CheckSlideVirtualInput()) {
            Pound();
        }
    }

    void FallingUpdate()
    {
        CheckDoubleJump();

        if(landed == true) {
            playerState = 0;
            return;
        }

        if(Input.GetButtonDown("Down") || CheckSlideVirtualInput()) {
            Pound();
        }
    }

    void SlideUpdate()
    {
        stateTimer += Time.deltaTime;
        //滚动时为玩家加速，x轴速度变化体现在屏幕坐标上
        camScript.setXSpeed(slideVec.x + camScript.cameraSpeed);

        if(stateTimer >= slideTime) {
            playerState = 0;
            stateTimer = 0.0f;
            camScript.setXSpeed(camScript.cameraSpeed);
        }

        if(Input.GetButtonDown("Up") || CheckJumpVirtualInput()) {
            Jump();
            stateTimer = 0.0f;
        }

        if(rigid.velocity.y < -0.01f) {
            playerState = 2;
        }
    }

    void DoubleJumpUpdate()
    {
        if(Input.GetButtonDown("Down") || CheckSlideVirtualInput()) {
            Pound();
        }

        if(rigid.velocity.y < 0.01f) {
            playerState = 2;
        }
    }

    void CheckDoubleJump()
    {
        if((Input.GetButtonDown("Up") || CheckJumpVirtualInput()) && !hasJumpedTwice) {
            rigid.velocity = doubleJumpVec;
            playerState = 5;
            hasJumpedTwice = true;
            jumpSound.clip = DoubleJumpAudio;
            jumpSound.Play();
        }
    }

    void PoundingUpdate()
    {
        if(landed == true) {
            playerState = 0;
        }
        //Debug.Log("POUND");
    }

    public void SetLanded(bool _landed)
    {
        landed = _landed;
    }

    void checkScreenRelativePosition()
    {
        float tx = transform.position.x;
        float left = leftNormX.position.x;
        float right = rightNormX.position.x;

        if(bossFighting) {
            left = leftBossNormX.position.x;
            right = rightBossNormX.position.x;
        }

        if(tx < left) {
            rigid.velocity = new Vector2(returnSpeed, rigid.velocity.y);
            //isHomed = false;
        }else if(tx > right) {
            rigid.velocity = new Vector2(-returnSpeed, rigid.velocity.y);
            //isHomed = false;
        }else {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            //isHomed = true;
        }
    }

    void getHurt()
    {
        //如果处在无敌时间，不执行受伤
        if(invincibleTime > 0) {
            return;
        }

        animator.Play("Hurt", 2);
        
        health --;
        if(health < 0) {
            health = 0;
        }

        switch (health)
        {
            case 2:
                healthDisplay.Play("Health 2", 1);
                break;
            case 1:
                healthDisplay.Play("Health 1", 1);
                break;
            case 0:
                //如果没有体力，游戏结束
                healthDisplay.Play("Health Bar Hidden", 1);
                playerState = 6;
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
                managerBehavior.gameOver();
                break;
            default:
                break;
        }

        invincibleTime = defaultInvincibleTimsc;
    }

    public void UpdateHealthBar()
    {
        switch (health)
        {
            case 3:
                healthDisplay.Play("Health Bar Fading", 1);
                break;
            case 2:
                healthDisplay.Play("Health 2", 1);
                break;
            case 1:
                healthDisplay.Play("Health 1", 1);
                break;
            case 0:
                healthDisplay.Play("Health Bar Hidden", 1);
                break;
            default:
                break;
        }
    }

    bool CheckJumpVirtualInput()
    {
        if(jumpPressed) {
            jumpPressed = false;
            return true;
        }else {
            return false;
        }
    }

    public void PressJump()
    {
        if(managerBehavior.GetGameState() == 0) {
            jumpPressed = true;
        }
    }

    public void PressSlide()
    {
        if(managerBehavior.GetGameState() == 0) {
            slidePressed = true;
        }
    }

    bool CheckSlideVirtualInput()
    {
        if(slidePressed) {
            slidePressed = false;
            return true;
        }else {
            return false;
        }
    }

    public void SetFootStep(bool isWood)
    {
        steppingOnWood = isWood;
    }

    void PlayFootstepSound()
    {
        if(steppingOnWood) {
            woodSound.Play();
        }else {
            grassSound.Play();
        }
    }

    void PlayScreamAudio()
    {
        screamSound.Play();
    }

    public bool isMaxHealth()
    {
        return health >= 3;
    }

    public void EnterBossFight()
    {
        bossFighting = true;
    }

    public void Victory()
    {
        invincibleTime = 99999;
        managerBehavior.gameOver();
    }
}