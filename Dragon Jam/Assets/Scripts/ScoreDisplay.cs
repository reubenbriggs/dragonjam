using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {

    public enum DisplayType
    {
        Running,
        Final
    }

    private Text display;
    [SerializeField]
    private DisplayType type;
    private float displayedValue;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();

        if(type == DisplayType.Final) {
            displayedValue = 0;
            GameManager.Instance.OnStateChange += OnStateChange;
        }
	}

    // Update is called once per frame
    void Update () {
        if(type == DisplayType.Running)
            display.text = Mathf.Floor(displayedValue = ScoreManager.Instance.Score) + " m";
	}

    private void OnStateChange(GameState current) {
        if (current == GameState.GameOver) {
            StartCoroutine(TotalScore(Mathf.Log(ScoreManager.Instance.Score, 2)));
        }
    }

    IEnumerator TotalScore(float time) {
        float t = 0;
        while(t < time) {
            displayedValue = Mathf.Lerp(displayedValue, ScoreManager.Instance.Score, t / time);
            display.text = Mathf.Ceil(displayedValue) + " m";
            t += Time.deltaTime;
            yield return null;
        }
        display.text = Mathf.Ceil(ScoreManager.Instance.Score) + " m";
    }

    public float GetDisplayedValue() {
        return Mathf.Ceil(displayedValue);
    }
}
