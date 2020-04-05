using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health=5;
    public int maxhealth=10;
	protected float animationTimer = 0;
    public Image[] hearts;
    protected Sprite[] fullheart;//= Resources.LoadAll<Sprite>("Sprites/hagheart.png");	
    protected Sprite emptyheart;
    public Sprite curr;

    // Start is called before the first frame update
    void Start()
    {
        fullheart = Resources.LoadAll<Sprite>("Sprites/hagheart");
        emptyheart = fullheart[3];
        // Debug.Log(fullheart);
        // Debug.Log(fullheart[1]);
    }

    // Update is called once per frame
    void Update()
    {
		if(health>maxhealth){
			health=maxhealth;
		}
    	animationTimer += Time.deltaTime;
		animationTimer %= 0.45f;
		curr=fullheart[(int)(animationTimer/0.15f)];
        for(int i=0; i < hearts.Length; i++){
        	if(i<health){
        		hearts[i].sprite=curr;
        	}
        	else{
        		hearts[i].sprite=emptyheart;	
        	}
    	    if(i<maxhealth){
    	    	hearts[i].enabled = true;
    	    }
    	    else{
    	    	hearts[i].enabled = false;
    	    }
        }
    }
}
