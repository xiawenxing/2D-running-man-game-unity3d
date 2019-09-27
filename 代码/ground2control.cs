using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground2control : MonoBehaviour
{
    // Use this for initialization
    GameObject[] grounds;
    int grnum = 6;
    public float speed = 2.0f;
    GameObject player;
    void Start()
    {
        grounds = new GameObject[grnum];
        grounds[0] = (GameObject)Resources.Load("Prefabs/gr1");
        grounds[1] = (GameObject)Resources.Load("Prefabs/gr2");
        grounds[2] = (GameObject)Resources.Load("Prefabs/gr3");
        grounds[3] = (GameObject)Resources.Load("Prefabs/gr4");
        grounds[4] = (GameObject)Resources.Load("Prefabs/gr5");
        grounds[5] = (GameObject)Resources.Load("Prefabs/gr6");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 地图移动
        Vector2 v = transform.localPosition;
        v.x -= (speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.F))
        {
            v.x -= (speed * Time.deltaTime);
        }
        // 新地形生成
        GameObject newgrd;
        Vector2[] pos;
        int len = transform.childCount;
        pos = new Vector2[len];
        int i;
        if (v.x < -51f)
        {
            i = 0;
            foreach (Transform grdchild in transform)
            {
                pos[i] = grdchild.localPosition;
                Object.Destroy(grdchild.gameObject, 0f);
                i++;
            }
            for (i = 0; i < len; i++)
            {
                newgrd = Instantiate(grounds[Random.Range(0, grnum)], transform) as GameObject;
                newgrd.transform.localPosition = pos[i];
                newgrd.transform.parent = transform;
                newgrd.transform.SetSiblingIndex(i);
            }
        }

        // 新地形循环
        if (v.x < -51f)
        {
            v.x = 40.2f;
        }
        if (player.transform.localPosition.y > -10f)
            transform.localPosition = v;
    }
}
