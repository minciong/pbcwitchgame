using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GenericController {
	protected private SpriteRenderer playerSpriteRenderer;
	protected private Rigidbody2D playerBody;
	public float jumpSpeed = 200f;
	public float accel = 20f; //value of increased speed for chosen direction
	public float xMaxVel = 8f; //maximum x velocity allowed
	public float sprintMult = 1.5f; //sprint multiplier
	public float sprintVal = 1f; //how much sprint currently
	public float duckRate = 20f; //negative y velocity from ducking mid-air
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

	//FixedUpdate works independent of frame rate, for interaction with the physics system
	void FixedUpdate () {

		var xForce = 0f; //these are to be appied to the playerBody before the function ends
		var yForce = 0f;
		var duckVal = 0f;

		if(Input.GetButton("Down")){
			duckVal -= duckRate;
			yForce = (-1)*duckRate; //add some negative force when trying to go down
		}

		if (Input.GetButton("Left")){
			if (playerBody.velocity.x >= (xMaxVel * -1)){
				xForce += (-1)*accel;
			}
		}

		if (Input.GetButton("Right")){
			if (playerBody.velocity.x <= xMaxVel){
				 xForce += accel;
			 }

		}

		if (Input.GetButton("Jump") && TerrainDistance(true) < 1.2f){
			yForce = jumpSpeed;
		}

		playerBody.AddForce(new Vector2( ( xForce*sprintVal ), yForce + duckVal) ); //apply all force as caluclated above
		duckVal = 0f;
	}

	// Update is called once per frame
	void Update () {
		var isMoving = false;
		var isDucking = false;

		if(Input.GetButtonDown("Sprint")){
			sprintVal = sprintMult;
			xMaxVel += 2;
		}

		if(Input.GetButtonUp("Sprint")){
			sprintVal = 1;
			xMaxVel -= 2;
		}

		//BEGIN: sprite logic

		if (Input.GetButton("Left")){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1); //flip sprite left
		}

		if (Input.GetButton("Right")){
			isMoving = true;
			transform.localScale = new Vector3(1,1,1);
		}


		if(Input.GetButton("Down")){
			isDucking = true;
		}

		if (isDucking){ //ducking overrides other sprites
			playerSpriteRenderer.sprite = duckSprite;
		}

		else if (TerrainDistance(true) >= 1.1f){ //if we're in the air, apply the jumping sprite
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

	}

	public void OnKillPlayer(){
		SceneManager.LoadScene("GameOverScene");
	}
}
