using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    protected bool useGravity;
    protected bool canMove = true;
    private Vector3 force;

	// Use this for initialization
	void Start () {
        force = new Vector3();
	}

    // Update is called once per frame
    protected void FixedUpdate () {
        if (canMove) {
            force = Vector3.ClampMagnitude(force, maxSpeed);
            if (useGravity) {
                force.y -= PhysicsManager.GravityValue * Time.fixedDeltaTime;
            }
            transform.position += force * Time.fixedDeltaTime;
        }
	}

    public void ResetForce() {
        force = Vector3.zero;
    }

    public void AddForce(Vector3 amount) {
        force += amount;
    }

    public void AddForceTowardsPosition(float amount, Vector3 position) {
        force += (position - transform.position).normalized * amount;
    }

    public void RemoveForce(float amount) {
        force = force.normalized * Mathf.Max(force.magnitude - amount, 0);
    }

    public void SetGravity(bool enabled) {
        useGravity = enabled;
    }

    public void Move(Vector3 amount) {
        transform.position += amount;
    }
}
