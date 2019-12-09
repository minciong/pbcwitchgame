using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {
	protected float movespeed = 5f;
	protected float jumpspeed = 10f;
	protected Sprite[] movesprites;
	protected Sprite standsprite;
	protected Sprite jumpsprite;
	protected Sprite ducksprite;
	protected float animationTimer = 0;
	// Use this for initialization
	void Start () {
		movesprites = Resources.LoadAll<Sprite>("Sprites/mario_moving");
		standsprite = Resources.Load<Sprite>("Sprites/mario_stand");
		jumpsprite = Resources.Load<Sprite>("Sprites/mario_jumping");
		ducksprite = Resources.Load<Sprite>("Sprites/mario_crouch");
	}
	
	// Update is called once per frame
	
	void Update () {
		var isMoving = false;
		var isDucking = false;
		//Debug.Log("floordistance: "+ FloorDistance());
		if(Input.GetKey(KeyCode.S)){
			isDucking = true;
		}else if(Input.GetAxis("Horizontal") < 0 && transform.position.x >= Camera.main.transform.position.x - 6){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1);
			transform.position = transform.position - Vector3.right * Time.deltaTime * movespeed;
		}else if(Input.GetAxis("Horizontal") > 0){
			isMoving = true;
			transform.localScale = new Vector3(1,1,1);
			transform.position = transform.position + Vector3.right * Time.deltaTime * movespeed;
		}
		if(Input.GetButtonDown("Jump") && FloorDistance() < 1.2f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpspeed);
			
		}
		if(isDucking){
			GetComponent<SpriteRenderer>().sprite = ducksprite;
		}else if(FloorDistance() >= 1.2f){
			GetComponent<SpriteRenderer>().sprite = jumpsprite;
		}else if(isMoving){
			animationTimer += Time.deltaTime;
			animationTimer %= 0.3f;
			GetComponent<SpriteRenderer>().sprite = movesprites[(int)(animationTimer/0.1f)];
		}else{
			GetComponent<SpriteRenderer>().sprite = standsprite;
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
