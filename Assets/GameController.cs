using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
	public int score = 10;
	public Text scoreText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var v3 = Input.mousePosition;
		v3 = Camera.main.ScreenToWorldPoint(v3);
// 		Debug.Log(v3);
		scoreText.text = "Score: " + score;
	}
	public void UpdateScore(int value){
		score+=value;
	}
	
}
