using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericController : MonoBehaviour {

	public float health { get; set; } = 100;
	public float damage { get; set; } = 10;

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

	public void OnCollisionEnter2D (Collision2D c){
		// Debug.Log(this.gameObject.name + " is colliding! " );
		var collider = c.collider.GetComponent<GenericController>();
		// Debug.Log(this.gameObject.name + "is colliding with " + c.gameObject.name);
		if(collider != null){ //if the collider exist
			this.health -= collider.damage; //subtract health from the collider's damage value
			if (this.health <= 0){
				Object.Destroy(this.gameObject);
			}
		}
	}

}
