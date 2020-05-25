using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    protected GameObject witchObject; // Witch Object
    protected PlayerController playerScript; //script within the witch object

    protected float boltMana = -1f;

    public Transform Castpoint;
    public GameObject boltPrefab;


    void Start()
    {
      witchObject = GameObject.Find("Player");
      playerScript = witchObject.GetComponent<PlayerController>(); //for use in adjusting mana values
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetButtonDown("Fire1") && playerScript.updateMana(boltMana)){
    		Shoot();
    	}
    }

    void Shoot(){
    	Instantiate(boltPrefab, Castpoint.position, Castpoint.rotation);
    }
}
