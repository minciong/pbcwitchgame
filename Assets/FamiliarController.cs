﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        float distance = Vector3.Distance(transform.position, transform.parent.gameObject.transform.position);
        // angle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(transform.position);
        var witchPos = transform.parent.gameObject.transform.position;
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouse.x, mouse.y, 0);
        // Debug.Log(transform.parent.gameObject.transform.position);
        // Debug.Log(distance);
    }
}
