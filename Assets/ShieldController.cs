using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject ColliderPrefab;

    protected TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
     
      trail = GetComponent<TrailRenderer>();
     
    }

    // Update is called once per frame
    void Update()
    {

    }
}
