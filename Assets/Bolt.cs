using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
	public float speed = 10f;
	public Rigidbody2D rb ;
	public float bulletgrowth = 10f;
	public float lifetime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right;
        if(Input.GetButton("Up")){
        	rb.velocity += new Vector2(0,1);
        }
        else if(Input.GetButton("Down")){
        	rb.velocity += new Vector2(0,-1);
        }
        rb.velocity = speed * rb.velocity.normalized;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
     	transform.localScale += new Vector3(bulletgrowth*Mathf.Sin(Time.deltaTime),
     										bulletgrowth*Mathf.Sin(Time.deltaTime),
     										bulletgrowth*Mathf.Sin(Time.deltaTime));  

    }
}
