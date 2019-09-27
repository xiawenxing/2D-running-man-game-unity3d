using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class playercontrol : MonoBehaviour
{
    public int HP = 3; // 生命值
    private int jumpCount = 0; // 跳跃的计数
    public int score = 0; // 分数
    private bool isjump = false; // 是否落地（关于跳跃计数清零）

    private Animator playerAni; // 动画播放器
    private AudioSource audio; // 音效播放器
    private Rigidbody2D rbody; // 刚体组件
    private Vector2 v; // 位置信息
    public GameObject player; // 游戏物体：玩家

    // 初始化
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();// 初始化物体刚体组件
        playerAni = GetComponent<Animator>(); // 初始化动画播放器（人物）
        audio = GetComponent<AudioSource>();// 初始化音效播放器
    }

    // Update is called once per frame
    void Update()
    {
        v = transform.localPosition; // 位置获取

        if (HP < 0) // 若生命为0，游戏结束
        {
            Die(); // 调用死亡处理
            v.y = -10f; // 将人物的位置定位在视图以下
            score = 0; // 分数清零
        }
        else if (v.y <= -7f) // 生命不为0，但是人物触发死亡条件
        {
            HP--; // 生命数减少（若减少后小于0，下一帧就会被检测出，所以不需要格外检验）
            v.y = 10f; // 人物更新到画面上方掉落
            Die(); // 调用死亡处理
            score = 0;// 分数清零
        }


        if (Input.GetKeyDown(KeyCode.J))// 接受到跳跃信息
        {
            Jump();// 跳跃
        }
        if (Input.GetKeyDown(KeyCode.F)) // 接受到加速信息
        {
            v.x += 4.0f * Time.deltaTime; // 加速
        }


        transform.localPosition = v; // 更新位置坐标
    }

    void Jump()// 人物的跳跃
    {
        jumpCount++; // 跳跃的计数
        if (jumpCount < 3 && HP >= 0) // 如果连跳次数在允许范围内
        {
            if(jumpCount==1)
                rbody.AddForce(Vector2.up * 380);// 实现跳跃
            else if(jumpCount==2)
                rbody.AddForce(Vector2.up * 250);// 实现二级跳跃
            playerAni.SetTrigger("StartJump"); // 播放跳跃动画
            AudioClip clip = Resources.Load<AudioClip>("jump");//加载跳跃音效
            audio.PlayOneShot(clip); // 播放音效
        }
    }

    void Die()// 人物死亡
    {
        playerAni.SetTrigger("Die");// 播放死亡动画
        AudioClip clip = Resources.Load<AudioClip>("die");// 播放死亡音效
        audio.PlayOneShot(clip);// 播放音效
    }

    void OnCollisionEnter2D(Collision2D colli)// 关于碰撞
    {

        if (colli.collider.tag == "ground")// 如果检测碰撞到地面
        {
            jumpCount = 0;// 连跳的计数清零
        }
        else if (colli.collider.tag == "tree")// 如果检测碰撞到数木
        {
            if (HP < 0)// 触发死亡条件
            {
                Die();
                score = 0;
                v.y = -10f;
            }
            else 
            {
                HP--;
                Die();
                score = 0;
                v.y = 10f;
            }
            transform.localPosition = v;
        }

    }
    void OnTriggerEnter2D(Collider2D colli)// 金币触发
    {
        if (colli.tag == "coin")// 碰撞对象为金币
        {
            score++; // 分数增加
            Object.Destroy(colli.gameObject); // 金币自动销毁
        }

    }
}
