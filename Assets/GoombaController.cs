using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {
	protected Vector3 direction = Vector3.right;
	protected float movespeed = 2.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + direction * Time.deltaTime * movespeed;
		if(WallDistance()<0.5f){
			direction = -direction;
		}
	}
	
	float WallDistance () {
		var results = new RaycastHit2D[1];
		var count = GetComponent<Collider2D>().Raycast(direction, results);
		if(count < 1){
			return 1000;
		}
		else{
			return results[0].distance;
		}
		
	}
	
	public void OnCollisionEnter2D (Collision2D c){
		
		var player = c.collider.GetComponent<PlayerController>();
		if(player != null){
			if(player.transform.position.y >transform.position.y+1){
				Destroy(gameObject);
			}
			
			else{
				//Destroy(player.gameObject);
				player.OnKillPlayer();
			}
		}
	}
}
