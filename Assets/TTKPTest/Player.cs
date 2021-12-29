using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 跳跃次数
    /// </summary>
    private int m_jumpCount = 0;

    private Animator m_ani;
    private Rigidbody2D m_rig;

    /// <summary>
    /// 一段跳速度
    /// </summary>
    public float JumpSpeed = 20;
    /// <summary>
    /// 二段跳速度
    /// </summary>
    public float SecondJumpSpeed = 15;

    void Start()
    {
        m_ani = gameObject.GetComponent<Animator>();
        m_rig = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // 按下空白键
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (0 == m_jumpCount)   //一段
            {
                m_ani.SetBool("isJumping", true);
                m_rig.velocity = new Vector2(0, JumpSpeed);
                ++m_jumpCount;
            }

        }
        // 按下空白键
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_ani.SetBool("isSliding", true);

        }
    }

    /// <summary>
    /// 碰撞事件方法
    /// </summary>
    /// <param name="other"></param>
	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            // 碰撞到地面
            m_ani.SetBool("isJumping", false);
            m_ani.SetBool("isSliding", false);

            m_jumpCount = 0;
        }

        if (other.gameObject.tag== "Border")
        {
            // 游戏结束
            GameMgr.instance.state = GameState.End;
            PlayerPrefs.DeleteAll();

        }

    }
    /// <summary>
    /// 触发器事件
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Coin" == collision.gameObject.tag)
        {
            // 吃到金币
            Destroy(collision.gameObject);
            GameMgr.instance.score++;
            PlayerPrefs.SetInt("coins", GameMgr.instance.score);
        }
    }
}
