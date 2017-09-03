using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {

    private Text display;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        display.text = Mathf.Floor(ScoreManager.Instance.Score) + "m";
	}
}
