using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
	protected int score = 10;
	public GameObject witchObject;
	public Text scoreText;
	public Text hpText;
	public Text mpText;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(10,10); //don't let the  player layer collide with itself (includes spells and such)

		Physics2D.IgnoreLayerCollision(11, 8); //don't allow the familiar to collide with terrain
		Physics2D.IgnoreLayerCollision(11, 9); //don't allow the familiar to collide with enemies

		//Create collision-void layer, only allow interaction with terrain in this layer
		Physics2D.IgnoreLayerCollision(31, 31); //layer interaction
		Physics2D.IgnoreLayerCollision(31, 10); //player
		Physics2D.IgnoreLayerCollision(31, 11); //familiar
		Physics2D.IgnoreLayerCollision(31, 9); //familiar
	}

	// Update is called once per frame
	void Update () {
		var v3 = Input.mousePosition;
		v3 = Camera.main.ScreenToWorldPoint(v3);
		//Debug.Log(v3);

		scoreText.text = "Score: " + score;
		hpText.text = "HP: " + witchObject.GetComponent<PlayerController>().health + " / " + witchObject.GetComponent<PlayerController>().maxHealth;
		mpText.text = "MP: " + witchObject.GetComponent<PlayerController>().mana + " / " + witchObject.GetComponent<PlayerController>().maxMana;
	 }
	// public void UpdateScore(int value){
	// 	score+=value;
	// }
	// public void UpdateHealth(int value){
	// 	witchCurrHealth = value;
	// }
	// public void UpdateMana(int value){
	// 	witchCurrMana = value;
	// }

}
