using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject followTarget;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

// 		if(followTarget.transform.position.x > transform.position.x){
			var position = transform.position;
			position.x = followTarget.transform.position.x;
			transform.position = position;
// 		}
	}
}
