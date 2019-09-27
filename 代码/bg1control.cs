using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg1control : MonoBehaviour {

    public float speed = 1.0f;// 背景移动的速度
    GameObject player;// 游戏对象：玩家

    // 初始化时调用
    void Start () {
        player = GameObject.Find("Player");// 获取玩家对象
    }
	
	// 每一帧更新时调用
	void Update () {
        Vector2 v = transform.localPosition; // 获取背景的坐标（相对）
        v.x -= (speed * Time.deltaTime); // x坐标左移（实现背景的滚动效果）

        if (Input.GetKeyDown(KeyCode.F)) // 得到加速信号
        {
            v.x -= 2*(speed * Time.deltaTime); // 加速滚动
        }

        if (v.x <= -59f) // 如果背景已经离开视图界面
        {
            v.x = -3.9f;// 则移动到第二张背景后（实现无限循环的滚动效果）
        }

        if (player.transform.localPosition.y > -10f)// 如果人物没有死亡
            transform.localPosition = v; // 更新坐标
        // 如果已近死亡，背景静止（不更新坐标）
	}
}
