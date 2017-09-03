using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    private static InputHandler instance;
    public static InputHandler Instance {
        get { return instance; }
    }
    public delegate void OnDragDelegate(Vector3 dragAmount);
    public OnDragDelegate onDrag;

    [SerializeField]
    private float maxDragTime;

    private bool dragging;
    private float dragStartTime;
    private Vector3 startPosition;
    private Touch lastTouch;
    private bool invertControls;


    // Use this for initialization
    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && !dragging) {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
            dragStartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && dragging) {
            Vector3 dragValue = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPosition);
            if (invertControls)
                dragValue = -dragValue;
            onDrag(dragValue / Mathf.Min(Time.time - dragStartTime, maxDragTime));
            dragging = false;
        }

#if UNITY_ANDROID
        if (Input.touchCount > 0 && !dragging) {
            startPosition = Input.GetTouch(0).position;
            dragging = true;
            lastTouch = Input.GetTouch(0);
        }
        if (Input.touchCount > 0)
            lastTouch = Input.GetTouch(0);
        if (Input.touchCount == 0 && dragging) {
            onDrag(lastTouch.position - (Vector2)startPosition);
            dragging = false;
        }
#endif
    }
}
