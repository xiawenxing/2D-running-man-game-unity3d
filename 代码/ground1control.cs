using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground1control : MonoBehaviour
{
    GameObject[] grounds;// 地面预设体池
    int grnum = 6; // 预设体数目
    public float speed = 2.0f; // 地面移动速度
    GameObject player; // 玩家

    // 初始化时调用
    void Start()
    {
        // 获取资源中的所有预设体
        grounds = new GameObject[grnum];
        grounds[0] = (GameObject)Resources.Load("Prefabs/gr1");
        grounds[1] = (GameObject)Resources.Load("Prefabs/gr2");
        grounds[2] = (GameObject)Resources.Load("Prefabs/gr3");
        grounds[3] = (GameObject)Resources.Load("Prefabs/gr4");
        grounds[4] = (GameObject)Resources.Load("Prefabs/gr5");
        grounds[5] = (GameObject)Resources.Load("Prefabs/gr6");
        // 获取玩家对象
        player = GameObject.Find("Player");
    }

    // 每一帧更新时调用
    void Update()
    {
        // 地图移动
        Vector2 v = transform.localPosition; // 整个画面地形的位置坐标
        v.x -= (speed * Time.deltaTime); // 移动
        if (Input.GetKeyDown(KeyCode.F)) // 加速信号
        {
            v.x -= (speed * Time.deltaTime); // 实现加速
        }

        // 新地形生成
        if (v.x < -45.7f) // 当地面移动出视图时，生成新地图
        {
            GroundGenerator();
        }
 
        // 新地形循环
        if (v.x < -45.7f) // 新地形循环到接下来出现
        {
            v.x = 43.4f;
        }

        if (player.transform.localPosition.y > -10f)// 如果玩家已经死亡，停止
            transform.localPosition = v;
    }

    // 地形生成（随机地形）
    /// <summary>
    /// 一个地形由四个随机的地面预设体组成。一共6个预设体（可增加），预设体有不同的位置、金币、灌木。
    /// </summary>
    void GroundGenerator()
    {
        GameObject newgrd; // 新地面对象
        Vector2[] pos;// 坐标数组
        int len = transform.childCount; // 地形中的地面个数
        pos = new Vector2[len];
        int i;
        i = 0;

        // 旧地面的销毁
        foreach (Transform grdchild in transform) // 地形中所有的现有地面子物体
        {
            pos[i] = grdchild.localPosition; // 记录下他们当前的位置信息
            Object.Destroy(grdchild.gameObject, 0f); // 销毁该物体
            i++;
        }

        // 新地面的生成
        for (i = 0; i < len; i++)
        {
            newgrd = Instantiate(grounds[Random.Range(0, grnum)], transform) as GameObject;// 实例化新的地面（随机）作为当前地形的子物体
            newgrd.transform.localPosition = pos[i];// 将该新地面的位置设置为适当位置
            newgrd.transform.parent = transform; // 设置新地面为当前地形的子物体
            newgrd.transform.SetSiblingIndex(i); // 设置地面的index
        }
    }
}
