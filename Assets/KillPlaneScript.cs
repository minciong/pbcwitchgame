using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D c){
		var player = c.GetComponent<PlayerController>();
		// var tagplayer = c.tag
		// if(player != null){
		//	player.OnKillPlayer();
		// }
		if(c.tag == "Player"){
			player.OnKillPlayer();
		}
	}
}
