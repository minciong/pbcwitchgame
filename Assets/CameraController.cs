using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform followTarget;
	public float cameraSpeed = 0.125f;
	public float sprintCameraSpeed = 1;
	public Vector3 offset;
	public float zoomIn = 7.0f; //camera when zoomed in during sprin
 	public float zoomOut = 9.0f; //camera default
	public float targetOrtho = 1.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Sprint")){
			targetOrtho = zoomIn;
			cameraSpeed += sprintCameraSpeed;
		}

		else if (Input.GetButtonUp("Sprint")) {
			targetOrtho = zoomOut;
			cameraSpeed -= sprintCameraSpeed;
		}


		Vector3 finalPosition = followTarget.position + offset;
		Vector3 smootherPosition = Vector3.Lerp (transform.position, finalPosition, cameraSpeed * Time.deltaTime);
		transform.position = smootherPosition;
	 	Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, cameraSpeed * Time.deltaTime);
	}
}
