using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : GenericController{
	protected Rigidbody2D rigidBody;
	protected Collider2D theCollider;

	public float speed = 20f;
	public float bulletgrowth = 10f;
	public float lifetime = 1f;

  public Sprite[] bulletSprites;
  protected SpriteRenderer boltSprite;
  protected float animationTimer = 0;

	protected int targetGoal = 2;

  // Start is called before the first frame update
  void Start()
  {
		//load rigidbody and Collider
		this.rigidBody = GetComponent<Rigidbody2D>();
		this.theCollider = GetComponent<Collider2D>();

		//load sprite resources
		bulletSprites = Resources.LoadAll<Sprite>("Sprites/SmokeBullet");
		boltSprite = GetComponent<SpriteRenderer>();

		this.damage = 1;

		//give the spell some velocity
    rigidBody.velocity = transform.right;
    if(Input.GetButton("Up")){
    	rigidBody.velocity += new Vector2(0,1);
    }
    else if(Input.GetButton("Down")){
    	rigidBody.velocity += new Vector2(0,-1);
    }
    rigidBody.velocity = rigidBody.velocity.normalized*speed;

		//Destroy object after lifetime
    Destroy(gameObject, lifetime);
  }

  // Update is called once per frame
  void Update(){

  // boltPrefab.Sprite = bulletSprites;
  animationTimer += Time.deltaTime;
  animationTimer %= lifetime;
  boltSprite.sprite = bulletSprites[(int)(animationTimer/(lifetime/5))];
 	transform.localScale += new Vector3(bulletgrowth* Time.deltaTime,
 										bulletgrowth* Time.deltaTime,
 										bulletgrowth* Time.deltaTime);

	}

	public void OnTriggerEnter2D(Collider2D otherCollider){
		//Remove a target from the goal
		this.targetGoal -= 1;

		//Add force to impact objected
		Rigidbody2D rb = otherCollider.GetComponent<Rigidbody2D>();
		GenericController otherController = otherCollider.GetComponent<GenericController>();

		if (rb && otherController){
			otherController.doDamage(this.damage);
			Vector2 moveDirection = this.rigidBody.transform.position - rb.transform.position;
			rb.AddForce(moveDirection * -1000f);
		}

		//When we hit our target number of collisions, allow anim to finish but stop
		if (targetGoal == 0){
			this.theCollider.enabled = false;
			this.rigidBody.velocity = Vector2.zero;
		}
	}
}
