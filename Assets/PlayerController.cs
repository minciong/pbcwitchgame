using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {
	protected float movespeed = 5f;
	protected float jumpspeed = 10f;
    protected float velocity = 0f;
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
		}else if(Input.GetButton("Left") && transform.position.x >= Camera.main.transform.position.x - 6){
			isMoving = true;
			transform.localScale = new Vector3(-1,1,1);
            if (velocity <= 5){ velocity -= 0.04f; }
		}else if(Input.GetButton("Right")){
			isMoving = true;
            if (velocity <= 5){ velocity += 0.04f; }
			transform.localScale = new Vector3(1,1,1);
		}
		if(Input.GetButton("Jump") && FloorDistance() < 1.2f){
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
		}
		else{
            if (velocity > 2)
                velocity -= 0.06f;
            else if ((velocity < 2 && velocity > 0) || (velocity > -2 && velocity < 0))
                velocity = 0;
            else if (velocity < 0)
                velocity += 0.06f;
            
			GetComponent<SpriteRenderer>().sprite = standsprite;
		}
		
        transform.position = transform.position + new Vector3(velocity, 0, 0) * Time.deltaTime;

		
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
