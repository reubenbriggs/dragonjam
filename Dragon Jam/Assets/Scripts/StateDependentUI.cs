using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDependentUI : MonoBehaviour {

    [SerializeField]
    private GameState requiredState;

	// Use this for initialization
	void Awake () {
        GameManager.Instance.OnStateChange += OnStateChange;
	}

    private void OnDestroy() {
        GameManager.Instance.OnStateChange -= OnStateChange;
    }

    void OnStateChange(GameState current) {
        if (current == requiredState)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
