using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericController : MonoBehaviour {

	public float health { get; set; } = 100;
	public float damage { get; set; } = 10;
	public float manaDamage { get; set; } = 10;

	//https://answers.unity.com/questions/1134985/sprite-blinking-effect-when-player-hit.html
	//Sprite blinking for our invicinbility frames
	private float spriteBlinkingTimer = 0.0f;
	private float spriteBlinkingMiniDuration = 0.1f;
	private float spriteBlinkingTotalTimer = 0.0f;
	private float spriteBlinkingTotalDuration = 1.0f;
	private bool startBlinking = false;

	private float knockTimer = 0.0f;
	private float knockDuration = 0.3f;

	private bool startKnock = false;
	private int oldLayer = -1;
	private Color transparent = new Color(1f,1f,1f,.5f);
	// private Color transparent = new Color(0.93f,0.55f,0.1f,.5f);//amber
	// private Color transparent = new Color(0.27f,1f,0f,.5f);//green
	private Color opaque = new Color(1f,1f,1f,1f);
	public float knockStrength = 1f;
	public Quaternion leftFacing = new Quaternion(0,180,0,0);
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
		this.startKnock = true;
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
	private void Knockback(){
		knockTimer+=Time.deltaTime;
		if(knockTimer>=knockDuration){
			startKnock=false;
			knockTimer=0.0f;
			this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			return;
		}
		
		if(this.gameObject.GetComponent<Rigidbody2D>().transform.rotation.y==0)
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2,1.5f)*knockStrength, ForceMode2D.Impulse);
		else
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2,1.5f)*knockStrength, ForceMode2D.Impulse);
	}


	public virtual void Update(){
		if(startBlinking == true) {
			SpriteBlinkingEffect();
		}
		if(startKnock){
			Knockback();
		}
  }

	public virtual void OnCollisionEnter2D (Collision2D c){
		GenericController collider = c.collider.GetComponent<GenericController>();
		if (collider != null)
			this.doDamage(collider.damage);
	}
}
