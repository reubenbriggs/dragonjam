using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject
{
    private bool inWater;
    private bool dragged;
    private Vector3 previousPosition;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start() {
        InputHandler.Instance.onDrag = OnDrag;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    new void Update() {
        base.Update();

        ScoreManager.Instance.UpdateScore(transform.position.y);
        if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main),spriteRenderer.bounds)) {
            //Do Death Stuff
        }
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
            useGravity = false;
            ResetForce();
            StartCoroutine(MoveToCentre(other.transform.position, 2f));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Water") {
            inWater = false;
            useGravity = true;
            dragged = false;
        }
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
