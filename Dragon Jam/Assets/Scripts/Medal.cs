using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class Medal : MonoBehaviour {

    [SerializeField]
    private Color gold, silver, bronze, none;
   
    [SerializeField]
    private ScoreDisplay linkedScoreDisplay;
    private Graphic graphic;
    private Animator anim;
    private float previousValue;

	// Use this for initialization
	void OnEnable () {
        graphic = GetComponent<Graphic>();
        anim = GetComponent<Animator>();
        graphic.color = none;
	}
	
	// Update is called once per frame
	void Update () {
        float value = linkedScoreDisplay.GetDisplayedValue();
        if (value >= 250 && previousValue < 250) {
            graphic.color = gold;
            anim.SetTrigger("Level");
        }
        else if (value >= 125 && previousValue < 125) {
            graphic.color = silver;
            anim.SetTrigger("Level");
        }
        else if (value >= 50 && previousValue < 50) {
            graphic.color = bronze;
            anim.SetTrigger("Level");
        }

        previousValue = value;
	}
}
