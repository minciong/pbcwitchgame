using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GenericController {
	protected float movespeed = 2.5f;
	protected float animationTimer = 0;
	protected int scorevalue = 100;
	protected Vector3 direction = -Vector3.right;
	// Use this for initialization
	void Start () {
		//change inherited values
		this.health = 10;
		this.damage = 1;
		this.manaDamage = 5;
	}

	protected override void onDamageAction(){return;} //disable onDamageAction

	// Update is called once per frame
	public override void Update () {
		transform.position = transform.position + direction * Time.deltaTime * movespeed;
		if(TerrainDistance(false)<0.5f){
			direction = -direction;
		}
		animationTimer += Time.deltaTime;
		animationTimer %= 0.5f;
		if(animationTimer <= 0.25f){
			transform.localScale = new Vector3(-1,1,1);
		}
		else{
			transform.localScale = new Vector3(1,1,1);
		}

	}
}
