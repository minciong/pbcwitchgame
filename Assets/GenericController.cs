using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericController : MonoBehaviour {

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

}
