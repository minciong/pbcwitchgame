using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour {
    public GameObject witchObject;
    protected PlayerController playerScript;

    public GameObject manaBar;
    protected Slider manaSlider;


    protected float animationTimer = 0;
    public Image[] hearts;
    protected Sprite[] fullheart;//= Resources.LoadAll<Sprite>("Sprites/hagheart.png");
    protected Sprite emptyheart;
    protected Sprite curr;

    protected float health;
    protected float mana;
    protected float maxHealth;
    protected float maxMana;

    // Start is called before the first frame update
    void Start(){
      manaBar = GameObject.Find("ManaBar");
      manaSlider = manaBar.GetComponent<Slider>();

      playerScript = witchObject.GetComponent<PlayerController>();

    	GameObject[] tmp = GameObject.FindGameObjectsWithTag("Heart"); //find all heart objects
      hearts = new Image[tmp.Length];
      for(int i=0; i < tmp.Length; i++){
    		hearts[i]=tmp[i].GetComponent<Image>();
    	}
        fullheart = Resources.LoadAll<Sprite>("Sprites/hagheart");
        emptyheart = fullheart[3];
      }

    // Update is called once per frame
    void Update(){
      health = playerScript.health;
      maxHealth = playerScript.maxHealth;

      manaSlider.maxValue = playerScript.maxMana;
      manaSlider.value = playerScript.mana;

      animationTimer += Time.deltaTime;
  		animationTimer %= 0.45f;
  		curr = fullheart[(int)(animationTimer/0.15f)];
      for(int i=0; i < hearts.Length; i++){
      	if (i < health){ //animate, if less health then display empty heart
      		hearts[i].sprite = curr;
      	}
      	else {
      		hearts[i].sprite = emptyheart;
      	}
        if (i < maxHealth){ //only display the amount of hearts we can have
  	    	hearts[i].enabled = true;
        }
        else {
  	    	hearts[i].enabled = false;
        }
      }
    }
  }
