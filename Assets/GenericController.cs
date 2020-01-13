using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericController : MonoBehaviour {

	protected float TerrainDistance (bool dir) { //direction to raycast, false for horizontal, true for vertical
    var mask = LayerMask.GetMask("Terrain"); //only check against Terrain layer
    var checkDirection = -Vector3.right;
    if (dir){checkDirection = -Vector3.up;}
		var results = new RaycastHit2D[1];
		var count = GetComponent<CapsuleCollider2D>().Raycast(checkDirection, results, mask);
		if(count < 1){
			return 1000;
		}
		else{
			return results[0].distance;
		}
  }

}
