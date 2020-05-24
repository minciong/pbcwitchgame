using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("GoBackToStart", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void GoBackToStart(){
		SceneManager.LoadScene("IntroScene");
	}
}
