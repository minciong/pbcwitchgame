using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
	public int score = 10;
	public int witchMaxHealth=5;
	public int witchMaxMana=100;
	public int witchCurrHealth=5;
	public int witchCurrMana=100;
	public Text scoreText;
	public Text hpText;
	public Text mpText;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var v3 = Input.mousePosition;
		v3 = Camera.main.ScreenToWorldPoint(v3);
		//Debug.Log(v3);

		scoreText.text = "Score: " + score;
		hpText.text = "HP: " + witchCurrHealth;
		mpText.text = "MP: " + witchCurrMana;
	}
	public void UpdateScore(int value){
		score+=value;
	}
	public void UpdateHealth(int value){
		witchCurrHealth = value;
	}
	public void UpdateMana(int value){
		witchCurrMana = value;
	}

}
