using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textcontrol : MonoBehaviour {
    GameObject player;// 玩家物体
    Text txt;// 文本组件

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        txt = GameObject.Find("Canvas/ScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        playercontrol pla = (playercontrol)player.GetComponent(typeof(playercontrol));
        txt.text = "HP:"+pla.HP.ToString()+"|Score:"+pla.score.ToString();// 更新信息
	}
}
