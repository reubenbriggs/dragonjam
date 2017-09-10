using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

    public delegate void AddGravity(float amount, Vector3 position);
    public event AddGravity ApplyGravity;

    [SerializeField]
    private float strength;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(ApplyGravity != null)
            ApplyGravity(strength * Time.deltaTime, transform.position);
	}

    private void OnTriggerEnter(Collider other) {
        PhysicsObject phys = other.GetComponent<PhysicsObject>();
        if (phys) {
            phys.SetGravity(false);
            ApplyGravity += phys.AddForceTowardsPosition;
        }
    }

    private void OnTriggerExit(Collider other) {
        PhysicsObject phys = other.GetComponent<PhysicsObject>();
        if (phys) {
            phys.SetGravity(true);
            ApplyGravity -= phys.AddForceTowardsPosition;
        }
    }
}
