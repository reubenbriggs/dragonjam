using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField]
    private Transform startTransform, endTransform;
    [SerializeField][Range(1,5)]
    private float duration;
    private float timer = 0;
    private Vector3 previousPosition;

    public delegate void MovementDelegate(Vector3 moveAmount);
    public event MovementDelegate OnMovement;
    

	// Use this for initialization
	void Start () {
        transform.position = startTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer <= duration) {
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, timer / duration);
            if(OnMovement != null)
                OnMovement(transform.position - previousPosition);
            timer += Time.deltaTime;
        }
        else {
            timer = 0;
            Transform temp = startTransform;
            startTransform = endTransform;
            endTransform = temp;
        }
        previousPosition = transform.position;
	}
}
