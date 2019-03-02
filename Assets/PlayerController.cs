using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {
	protected float movespeed = 5f;
	protected float jumpspeed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("floordistance: "+ FloorDistance());
		if(Input.GetKey(KeyCode.A)){
			transform.position = transform.position - Vector3.right * Time.deltaTime * movespeed;
		}
		if(Input.GetKey(KeyCode.D)){
			transform.position = transform.position + Vector3.right * Time.deltaTime * movespeed;
		}
		if(Input.GetKeyDown(KeyCode.W) && FloorDistance() < 1.2f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpspeed);
			
		}
		if(Input.GetKey(KeyCode.S)){
			
		}
	}
	float FloorDistance () {
		var results = new RaycastHit2D[1];
		var count = GetComponent<Collider2D>().Raycast(-Vector2.up, results);
		if(count < 1){
			return 1000;
		}
		else{
			return results[0].distance;
		}
		
	}
	public void OnKillPlayer(){
		SceneManager.LoadScene("GameOverScene");
	}
}
