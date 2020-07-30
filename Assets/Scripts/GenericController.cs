using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericController : MonoBehaviour {

	public float health { get; set; } = 100;
	public float damage { get; set; } = 10;

	//https://answers.unity.com/questions/1134985/sprite-blinking-effect-when-player-hit.html
	//Sprite blinking for our invicinbility frames
	private float spriteBlinkingTimer = 0.0f;
	private float spriteBlinkingMiniDuration = 0.1f;
	private float spriteBlinkingTotalTimer = 0.0f;
	private float spriteBlinkingTotalDuration = 1.0f;
	private bool startBlinking = false;
	private int oldLayer = -1;
	private Color transparent = new Color(1f,1f,1f,.5f);
	// private Color transparent = new Color(0.93f,0.55f,0.1f,.5f);
	// private Color transparent = new Color(0.27f,1f,0f,.5f);
	private Color opaque = new Color(1f,1f,1f,1f);
	public int knockStrength = 2;
	protected float TerrainDistance (bool dir) { //direction to raycast, false for horizontal, true for vertical
    var mask = LayerMask.GetMask("Terrain"); //only check against Terrain layer
    Vector2 checkDirection = -Vector2.right; //check from horizontally
    if (dir){checkDirection = -Vector2.up;} //unless dir is true

		//Defined As:
		//public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity);
		RaycastHit2D results = Physics2D.Raycast(transform.position, checkDirection, Mathf.Infinity, mask);
		if (results.collider != null){
			return results.distance;
		}
		else{
			return Mathf.Infinity;
		}
  }

	protected virtual void onDeathAction(){ //by default, destroy the GameObject
		Object.Destroy(this.gameObject);
	}

	protected virtual void onDamageAction(){
		this.oldLayer = this.gameObject.layer;
		this.gameObject.layer = 31;
		this.startBlinking = true;
	}

	public void doDamage(float damageVal){ //by default, destroy the GameObject
		if (damageVal > 0){
			onDamageAction();
			this.health -= damageVal; //subtract health from the collider's damage value
			if (this.health <= 0){
				onDeathAction();
			}
		}
	}

	private void SpriteBlinkingEffect(){
		spriteBlinkingTotalTimer += Time.deltaTime;
		if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration){
				startBlinking = false;
			 	spriteBlinkingTotalTimer = 0.0f;
			 	this.gameObject.GetComponent<SpriteRenderer> ().color = this.opaque;
			 	this.gameObject.layer = this.oldLayer; //re-enable collision
			 	return;
		}

		spriteBlinkingTimer += Time.deltaTime;
		if (spriteBlinkingTimer >= spriteBlinkingMiniDuration){
		 spriteBlinkingTimer = 0.0f;
		 if (this.gameObject.GetComponent<SpriteRenderer> ().color == this.opaque) {
				 this.gameObject.GetComponent<SpriteRenderer> ().color = this.transparent;  //make changes
		 }
		 else {
				 this.gameObject.GetComponent<SpriteRenderer> ().color = this.opaque;   //make changes
		 }
		}
	}


	public virtual void Update(){
		if(startBlinking == true) {
			SpriteBlinkingEffect();
		}
  }

	public virtual void OnCollisionEnter2D (Collision2D c){
		GenericController collider = c.collider.GetComponent<GenericController>();
		if (collider != null)
			this.doDamage(collider.damage);
	}
}
