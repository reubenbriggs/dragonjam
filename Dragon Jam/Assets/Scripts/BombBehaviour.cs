using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

    [SerializeField]
    private ParticleSystem[] particles;
    private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Explode();
            GameManager.Instance.GameOver();
        }
    }

    void Explode() {
        rend.enabled = false;
        foreach (ParticleSystem p in particles) {
            p.Play();
        }
    }
}
