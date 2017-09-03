using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    private bool inWater;
    private bool dragged;
    private Vector3 previousPosition;

    // Use this for initialization
    void Start() {
        InputHandler.Instance.onDrag = OnDrag;
    }

    // Update is called once per frame
    new void Update() {
        base.Update();

        Vector3 dir = transform.position - previousPosition;
        if (dir != Vector3.zero) {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        previousPosition = transform.position;
    }

    void OnDrag(Vector3 forceToAdd) {
        if (inWater) {
            dragged = true;
            AddForce(forceToAdd);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Water") {
            inWater = true;
            ResetForce();
            StartCoroutine(MoveToCentre(other.transform.position, 2f));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Water")
            inWater = false;
    }

    IEnumerator MoveToCentre(Vector3 targetPosition, float time) {
        float t = time;
        while (t > 0 && !dragged) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (time - t) / time);
            t -= Time.deltaTime;
            yield return null;
        }
        dragged = false;
    }
}
