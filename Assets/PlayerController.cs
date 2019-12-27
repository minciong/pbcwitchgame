using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	private SpriteRenderer playerSpriteRenderer;
	private Rigidbody2D playerBody;
	public float jumpSpeed = 60f;
	public float accel = 5f; //value of increased speed for chosen direction
	public float xMaxVel = 6f; //maximum x velocity allowed
	public float sprintMult = 1.5f; //sprint multiplier
	public float sprintVal = 1f; //how much sprint currently
	public float duckRate = 10f; //negative y velocity from ducking mid-air
	public float animationTimer = 0;
	protected Sprite[] moveSprites;
	protected Sprite standSprite;
	protected Sprite jumpSprite;
	protected Sprite duckSprite;
	// Use this for initialization
	void Start () {
		moveSprites = Resources.LoadAll<Sprite>("Sprites/mario_moving");
		standSprite = Resources.Load<Sprite>("Sprites/mario_stand");
		jumpSprite = Resources.Load<Sprite>("Sprites/mario_jumping");
		duckSprite = Resources.Load<Sprite>("Sprites/mario_crouch");

		playerSpriteRenderer = GetComponent<SpriteRenderer>();
		playerBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		var isMoving = false;
		var isDucking = false;
		var xForce = 0f; //these are to be appied to the playerBody before the function ends
		var yForce = 0f;
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

	  if (Input.GetButton("Left") && transform.position.x >= Camera.main.transform.position.x - 6){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1);
	          if (playerBody.velocity.x >= (xMaxVel * -1)){
							xForce += (-1)*accel;
						}
		}
		if (Input.GetButton("Right")){
			isMoving = true;
	          if (playerBody.velocity.x <= xMaxVel){
							 xForce += accel;
						 }
			transform.localScale = new Vector3(1,1,1);
		}

		if (Input.GetButton("Jump") && FloorDistance() < 1.2f){
			yForce = jumpSpeed;
		}

		if (isDucking){
			yForce = (-1)*duckRate; //add some negative force when trying to go down
		}


		//BEGIN: sprite logic
		if (isDucking){ //ducking overrides other sprites
			playerSpriteRenderer.sprite = duckSprite;
		}

		else if (FloorDistance() >= 1.1f){ //if we're in the air, apply the jumping sprite
			playerSpriteRenderer.sprite = jumpSprite;
		}

		else if (isMoving){ //if we're moving otherwise, regular walk sprites
			animationTimer += Time.deltaTime;
			animationTimer %= 0.3f;
			playerSpriteRenderer.sprite = moveSprites[(int)(animationTimer/0.1f)];
		}

		else { //apply the stand sprite if doing nothing else
			playerSpriteRenderer.sprite = standSprite;
		}

		//END: sprite logic

		playerBody.AddForce(new Vector2( ( xForce*sprintVal ), yForce + duckVal) ); //apply all force as caluclated above
		duckVal = 0f;

	}

	float FloorDistance () {
		var results = new RaycastHit2D[1];
		var count = GetComponent<CapsuleCollider2D>().Raycast(-Vector2.up, results);
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
