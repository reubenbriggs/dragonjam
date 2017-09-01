using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    private bool inWater;

    // Use this for initialization
    void Start() {
        InputHandler.Instance.onDrag = OnDrag;
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    void OnDrag(Vector3 forceToAdd) {
        if (inWater)
            AddForce(forceToAdd);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Water") {
            inWater = true;
            ResetForce();
            StartCoroutine(MoveToCentre(other.transform.position, 0.5f));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Water")
            inWater = false;
    }

    IEnumerator MoveToCentre(Vector3 targetPosition, float time) {
        float t = time;
        while (t > 0) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (time - t) / time);
            t -= Time.deltaTime;
            yield return null;
        }
    }
}
