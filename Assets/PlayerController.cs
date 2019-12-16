using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {
	protected float jumpSpeed = 10f;
    protected float xVel = 0f; //stores horizontal current velocity
    protected float accel = 0.04f; //value of increased speed for chosen direction
    protected float xStopRate = 0.1f; //rate to slow down at when not inputting
    protected float xStopRange = 2f; //how close to this value in or der to full stop
    protected float xMaxVel = 4f; //maximum x velocity allowed
    protected float sprintMult = 1.5f; //sprint multiplier
    protected float sprintVal = 1f; //how much sprint currently
    protected float yVel = 0f; //y velocity
    protected float duckRate = 0.1f; //negative y velocity from ducking mid-air
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
		if(Input.GetButton("Down")){
			isDucking = true;
            yVel -= duckRate;
            
		}
        if(Input.GetButtonDown("Sprint")){
			sprintVal = sprintMult;
            xMaxVel += 2;
        }
        else if(Input.GetButtonUp("Sprint")){
			sprintVal = 1;
            xMaxVel -= 2;
        }
        if(Input.GetButton("Left") && transform.position.x >= Camera.main.transform.position.x - 6){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1);
            if (xVel >= (xMaxVel * -1)){ xVel -= accel; }
		}
		if(Input.GetButton("Right")){
			isMoving = true;
            if (xVel <= xMaxVel){ xVel += accel; } 
			transform.localScale = new Vector3(1,1,1);
		}
		if(Input.GetButton("Jump") && FloorDistance() < 1.2f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpSpeed);
			
		}
				
		if(isDucking){
			GetComponent<SpriteRenderer>().sprite = ducksprite;
            yVel -= duckRate;
		}else if(FloorDistance() >= 1.2f){
			GetComponent<SpriteRenderer>().sprite = jumpsprite;
        }if(FloorDistance() >= 0.05){
			yVel = 0;
		}if(isMoving){
			animationTimer += Time.deltaTime;
			animationTimer %= 0.3f;
			GetComponent<SpriteRenderer>().sprite = movesprites[(int)(animationTimer/0.1f)];
		}
		else{
            if (xVel > xStopRange) //if we've got positive xVel, slow down
                xVel -= xStopRate;
            else if ((xVel < xStopRange && xVel > 0) || (xVel > (xStopRange * -1) && xVel < 0)) //if we're close enough to stop, stop
                xVel = 0; 
            else if (xVel < 0) //if negative xVel, increase
                xVel += xStopRate;
            
			GetComponent<SpriteRenderer>().sprite = standsprite;
		}
        transform.position = transform.position + (new Vector3(xVel, yVel, 0) * sprintVal) * Time.deltaTime;

		
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
