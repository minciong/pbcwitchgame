using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform followTarget;
	public float cameraSpeed = 5f;
	public float sprintCameraSpeed = 5f;
	public Vector3 offset = new Vector3(0, 2, -1);
	public float zoomIn = 7.0f; //camera when zoomed in during sprin
 	public float zoomOut = 9.0f; //camera default
	public float targetOrtho = 9.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// if (Input.GetButtonDown("Sprint")){
		// 	targetOrtho = zoomIn;
		// 	cameraSpeed += sprintCameraSpeed;
		// }

		// else if (Input.GetButtonUp("Sprint")) {
		// 	targetOrtho = zoomOut;
		// 	cameraSpeed -= sprintCameraSpeed;
		// }


		Vector3 finalPosition = followTarget.position + offset;
		Vector3 smootherPosition = Vector3.Lerp (transform.position, finalPosition, cameraSpeed * Time.deltaTime);
		transform.position = smootherPosition;
	 	Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, cameraSpeed * Time.deltaTime);
	}
}
