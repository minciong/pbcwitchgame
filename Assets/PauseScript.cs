using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause")){
            paused = !paused;
        }
        if(paused){
            Time.timeScale = 0;
            GetComponent<Weapon>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Familiar").GetComponent<FamiliarController>().enabled = false;
        } else {
            Time.timeScale = 1;
            GetComponent<Weapon>().enabled = true;
            GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Familiar").GetComponent<FamiliarController>().enabled = true;
        }
    }
}