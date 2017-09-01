using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    [SerializeField]
    private float maxSpeed;
    private Vector3 force;

	// Use this for initialization
	void Start () {
        force = new Vector3();
	}
	
	// Update is called once per frame
	protected void Update () {
        force = Vector3.ClampMagnitude(force, maxSpeed);
        transform.position += force * Time.deltaTime;
	}

    public void ResetForce() {
        force = Vector3.zero;
    }

    public void AddForce(Vector3 amount) {
        force += amount;
    }

    public void RemoveForce(float amount) {
        force = force.normalized * Mathf.Max(force.magnitude - amount, 0);
    }
}
