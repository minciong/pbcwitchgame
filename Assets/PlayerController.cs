﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GenericController {
	protected private SpriteRenderer playerSpriteRenderer;
	protected private Rigidbody2D playerBody;
	public float jumpForce = 13f;
	protected float xVelocity = 0;
	public float accel = 30f; //value of increased speed for chosen direction
	public float decel = 30f; //value of decreased speed for chosen direction
	public float xMaxVel = 10f; //maximum x velocity allowed
	public float sprintMult = 1.5f; //sprint multiplier
	public float sprintVal = 1f; //how much sprint currently
	public float duckRate = 0.05f; //negative y velocity from ducking mid-air
	public float animationTimer = 0;
	public float jumpTime = 0;
	protected bool isJumping;
	protected float jumpTimeCounter;
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
	// void FixedUpdate () {
	// }

	// Update is called once per frame
	void Update () {
		var isMoving = false;
		var isDucking = false;
		if(Input.GetButton("Down")){
			playerBody.velocity += duckRate * Vector2.down; //Increases falling speed
		}
		// Initial Jump
		if (Input.GetButtonDown("Jump") && TerrainDistance(true) < 1.2f){
			isJumping = true;
 			playerBody.velocity = Vector2.up * jumpForce;
 			jumpTimeCounter = jumpTime;
		}
		// jump higher while held
		if (Input.GetButton("Jump") && isJumping){
			if(jumpTimeCounter > 0){

			 	playerBody.velocity = Vector2.up * jumpForce;
			 	jumpTimeCounter -= Time.deltaTime;
			}
			else{
				isJumping = false;
			}
		}
		// Jump not being held
		if (Input.GetButtonUp("Jump")){
			isJumping = false;
		}
		// Acceleration
		if (Input.GetButtonDown("Sprint")){
			sprintVal = sprintMult;
			xMaxVel *= sprintMult;
		}

		if (Input.GetButtonUp("Sprint")){
			sprintVal = 1;
			xMaxVel /= sprintMult;
		}
		if ((Input.GetButton("Left"))&&(xVelocity > -xMaxVel))
		    xVelocity = xVelocity - accel * Time.deltaTime;

		else if ((Input.GetButton("Right"))&&(xVelocity < xMaxVel))
		    xVelocity = xVelocity + accel * Time.deltaTime;
		else
		{
		    if (xVelocity > decel * Time.deltaTime)
		        xVelocity = xVelocity - decel * Time.deltaTime;
		    else if (xVelocity < -decel * Time.deltaTime)
		        xVelocity = xVelocity + decel * Time.deltaTime;
		    else
		        xVelocity = 0;
		}

		transform.position = transform.position + Vector3.right * xVelocity * Time.deltaTime;


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
