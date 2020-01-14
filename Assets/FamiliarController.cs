using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarController : MonoBehaviour
{
    public float radius = 3;
    protected bool teleported = false;
    public Transform witch_character; // Witch Object
    // ROTATE PROPERTY OF TELEPORT
    // https://answers.unity.com/questions/1164022/move-a-2d-item-in-a-circle-around-a-fixed-point.html
    public float RotateSpeed = 10f;
    public float accelerate = 0;
    public float accelerate_step = 12;
    public float maxspeed = 2000f;
    public bool teleport_cd = false;
    public bool rotate = false;
    public int teleport_cd_duration = 0;
    protected bool teleportOverride = false; //prevents teleports if true

    // private Vector2 _centre;
    public float _angle;

    // Start is called before the first frame update
    void Start()
    {

    }


   void OnCollisionEnter2D(Collision2D collision){
    if (collision.collider.name == "TerrainTilemap"){ //when we collide with the tilemap, no teleports
      teleportOverride = true;
    }
   }

   void OnCollisionExit2D(Collision2D collision){
     if (collision.collider.name == "TerrainTilemap"){ //stopped colliding with the tilemap, teleport allowed
       teleportOverride = false;
     }
   }

    // Update is called once per frame
    void Update()
    {

      if (Input.GetButton("familiarSwap"))
        rotate = !rotate;

      Camera camera = GetComponent<Camera>();
      float distance = Vector3.Distance(transform.position, witch_character.gameObject.transform.position);
      // angle = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // Debug.Log(transform.position);
      var witchPos = witch_character.gameObject.transform.position;
      var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      var direction = new Vector3(mouse.x, mouse.y, 0) - witchPos;
      Vector3 new_pos;

      //LINEAR TRANSFORM
      if (!rotate) {
        if (Mathf.Abs(direction.x) + Mathf.Abs(direction.y) > radius){
            new_pos = witchPos + Vector3.Normalize(direction) * radius;
        }
        else
        {
            new_pos = witchPos + direction;
        }
      }

      //ROTATIONAL TRANSFORM
      else {
        new_pos = witchPos + Vector3.Normalize(direction) * radius;
      }


      // Debug.Log(witch_character.gameObject.transform.position);
      // Debug.Log(distance);
      if (Input.GetButtonDown("Teleport") && !teleported && !teleport_cd && !teleportOverride)
      {
          var temp = witchPos;
          Vector3 targetDir = witch_character.position - transform.position;
          _angle = (Vector3.SignedAngle(targetDir, transform.up, transform.forward)/180) * Mathf.PI;
          witch_character.position = transform.position;
          transform.position = temp;
          teleported = true;
          teleport_cd = true;
      }
      if (teleported)
      {
          // LINEAR TRANSFORM
          if (!rotate){
            if (accelerate < maxspeed) { accelerate += Time.deltaTime / 10; }
            transform.position = Vector3.Lerp(transform.position, new_pos, accelerate);
          }

          else {
            // ANGULAR TRANSFORM
            _angle += RotateSpeed * Time.deltaTime*(accelerate/100);
            if(accelerate < maxspeed){ accelerate += accelerate_step; }
            var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle), 0) * radius;
            transform.position = witch_character.position + offset;
          }
            var difference = transform.position - new_pos;

            if (Mathf.Abs(difference.x) + Mathf.Abs(difference.y) < 1)
            {
                teleported = false;
            }
      }
      else
      {
          transform.position = new_pos;
          accelerate = 0;
      }
      if (teleport_cd_duration < 50 && teleport_cd) { teleport_cd_duration += 1; }
      else { teleport_cd = false; teleport_cd_duration = 0; }
  }

}
