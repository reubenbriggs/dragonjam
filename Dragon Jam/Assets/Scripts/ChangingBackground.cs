using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangingBackground : MonoBehaviour {

    [SerializeField]
    private Gradient colourChange;
    [SerializeField]
    private float maxedHeight;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colourChange.Evaluate(0);
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.color = colourChange.Evaluate(ScoreManager.Instance.Score / maxedHeight);
	}
}
