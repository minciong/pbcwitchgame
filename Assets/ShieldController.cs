using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Transform trailFollower; //the object to follow
    public EdgeCollider2D edgeCollider; //the edgecollider
    public TrailRenderer trail; //our trail renderer (shieldRenderer)
    const int MAX_POSITIONS = 100;
    protected Vector3[] trailPoints = new Vector3[MAX_POSITIONS];
    protected Vector2 startLocation; //stores drawing start position

    // Start is called before the first frame update
    void Start()
    {
      edgeCollider = GetComponent<EdgeCollider2D>(); //get the edge collider, build into correct shape
      trail = GetComponent<TrailRenderer>();
      trailFollower = this.transform.parent; //get the familiar
      Vector2 startLocation = trailFollower.transform.position;
      edgeCollider.enabled = false; //avoid colliding with anything until drawn
    }

    // Update is called once per frame
    void Update(){
      if (Input.GetButtonUp("DrawShield")){
        trail.emitting = false;
        int trailPointsNum = trail.GetPositions(trailPoints); //get actual number of points and populate array
        Vector2[] v2 = new Vector2[trailPointsNum];
        int i = 0;
        foreach (Vector3 point in trailPoints){ //convert v3 array to v2 array
          if (i == trailPointsNum){break;} //if we've filled our array
          v2[i] = point;
          i++;
        }
        edgeCollider.enabled = true;
        this.transform.position = startLocation;
        edgeCollider.points = v2;
      }
    }
}
