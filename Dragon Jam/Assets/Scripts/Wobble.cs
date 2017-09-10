using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour {

    [SerializeField]
    private float rotationVariation;
    [SerializeField]
    private float duration;
    private float offset;

    private void Start() {
        offset = Random.Range(0, 10);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = transform.eulerAngles;
        rot.z = rotationVariation * Mathf.Sin((Time.time + offset) / duration);
        transform.eulerAngles = rot;
	}
}
