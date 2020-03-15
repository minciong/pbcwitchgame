using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Castpoint;
    public GameObject boltPrefab;
    // Update is called once per frame
    void Update()
    {
    	if(Input.GetButtonDown("Fire1")){
    		Shoot();
    	}   
    }
    void Shoot(){
    	Instantiate(boltPrefab,Castpoint.position,Castpoint.rotation);
    }
}
