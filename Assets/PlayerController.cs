using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D playerBody;
	protected float jumpSpeed = 50f;
	protected float accel = 5f; //value of increased speed for chosen direction
	protected float xMaxVel = 6f; //maximum x velocity allowed
	protected float sprintMult = 1.5f; //sprint multiplier
	protected float sprintVal = 1f; //how much sprint currently
	protected float duckRate = 10f; //negative y velocity from ducking mid-air
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

		playerBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame

	void Update () {
		var isMoving = false;
		var isDucking = false;
		var xForce = 0f;
		var yForce = 0f;
		var forceStop = 1f;
		var duckVal = 0f;

		if(Input.GetButtonDown("Sprint")){
			sprintVal = sprintMult;
	    xMaxVel += 2;
	  }

		if(Input.GetButtonUp("Sprint")){
			sprintVal = 1;
			xMaxVel -= 2;
		}

		if(Input.GetButton("Down")){
			isDucking = true;
	    duckVal -= duckRate;
		}

	  if(Input.GetButton("Left") && transform.position.x >= Camera.main.transform.position.x - 6){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1);
	          if (playerBody.velocity.x >= (xMaxVel * -1)){
							xForce += (-1)*accel;
						}
		}
		if(Input.GetButton("Right")){
			isMoving = true;
	          if (playerBody.velocity.x <= xMaxVel){
							 xForce += accel;
						 }
			transform.localScale = new Vector3(1,1,1);
		}

		if(Input.GetButton("Jump") && FloorDistance() < 1.2f){
			yForce = jumpSpeed;
			//GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpSpeed);

		}

		if(FloorDistance() >= 1.2f){
			GetComponent<SpriteRenderer>().sprite = jumpsprite;
		}

		if(isDucking){
			GetComponent<SpriteRenderer>().sprite = ducksprite;
			yForce = (-1)*duckRate;
		}

		if(isMoving){
			animationTimer += Time.deltaTime;
			animationTimer %= 0.3f;
			GetComponent<SpriteRenderer>().sprite = movesprites[(int)(animationTimer/0.1f)];
		}
		else {
			GetComponent<SpriteRenderer>().sprite = standsprite;
		}
				Debug.Log("xForce: "+ xForce);
				Debug.Log("xMaxVel: "+ xMaxVel);
				Debug.Log("xStopRate: "+ xStopRate);
				Debug.Log("forceStop: "+ forceStop);
				Debug.Log("Calculated xForce:" +  (xForce*sprintVal) * forceStop);
				playerBody.AddForce(new Vector2( ( (xForce*sprintVal) * forceStop ), yForce + duckVal) );
				forceStop = 1f;
				duckVal = 0f;
	      //transform.position = transform.position + (new Vector3(xVel, yVel, 0) * sprintVal) * Time.deltaTime;


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
