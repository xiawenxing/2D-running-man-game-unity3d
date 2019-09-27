using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg2control : MonoBehaviour
{

    public float speed = 1.0f;
    GameObject player;

    // Use this for initialization

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = transform.localPosition;
        v.x -= (speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.F))
        {
            v.x -= (speed * Time.deltaTime);
        }
        if (v.x <= -60.6f)
        {
            v.x = 2.6f;
        }
        if (player.transform.localPosition.y > -10f)
            transform.localPosition = v;
    }
}
