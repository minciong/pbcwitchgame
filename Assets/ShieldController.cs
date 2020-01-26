using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Transform trailFollower; //the object to follow
    public EdgeCollider2D edgeCollider; //the edgecollider
    public TrailRenderer trail; //our trail renderer (shieldRenderer)
    const int MAX_POSITIONS = 100;
    protected Vector3[] trailPoints;
    protected Vector2 startLocation; //stores drawing start position

    // Start is called before the first frame update
    void Start()
    {
      edgeCollider = GetComponent<EdgeCollider2D>(); //get the edge collider, build into correct shape
      trail = GetComponent<TrailRenderer>();
      trailFollower = this.transform.parent; //get the familiar
      startLocation = trailFollower.transform.position; //needed to fix offset issues later
      edgeCollider.enabled = false; //avoid colliding with anything until drawn
    }

    // Update is called once per frame
    void Update(){
      if (Input.GetButtonUp("DrawShield")){
        int trailPointNum = trail.positionCount;
        trailPoints = new Vector3[trailPointNum];
        trail.GetPositions(trailPoints); //populate array
        Vector2[] edgePoints = new Vector2[trailPointNum];
        int i = 0;
        foreach (Vector3 point in trailPoints){ //convert v3 array to v2 array
          Debug.Log(point);
          edgePoints[i] = point;
          i++;
        }
        edgeCollider.enabled = true;
        edgeCollider.offset = -this.transform.position; //fixes offset to origin issue
        edgeCollider.points = edgePoints;
      }
    }
}
