using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class StateTransitionUI : MonoBehaviour {

    private Graphic ui;
    [SerializeField]
    private float transitionTime;
    private bool playing;

	// Use this for initialization
	void Start () {
        ui = GetComponent<Graphic>();
        GameManager.Instance.OnStateChange += OnStateChange;
	}
	
    void OnStateChange(GameState current) {
        if(!playing)
            StartCoroutine(Transition());
    }

    IEnumerator Transition() {
        playing = true;
        ui.CrossFadeAlpha(1f, transitionTime / 2, true);
        yield return new WaitForSeconds(transitionTime / 2);
        ui.CrossFadeAlpha(0f, transitionTime / 2, true);
        playing = false;
    }
}
