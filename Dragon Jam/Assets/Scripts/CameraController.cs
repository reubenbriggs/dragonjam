using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    [SerializeField]
    private float offset;
    [SerializeField]
    private float followRate;
    [SerializeField]
    private Transform target;

    private float minY;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        offset = cam.orthographicSize / 2;
        Vector3 pos = transform.position;
        pos.y = target.position.y + offset;
        minY = pos.y;
        transform.position = pos;
    }
	
	// Update is called once per frame
	void Update () {
        offset = cam.orthographicSize / 2;
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(transform.position.y, target.position.y + offset, (1 / followRate) * Time.deltaTime);
        if (pos.y > minY)
            minY = pos.y;
        pos.y = Mathf.Max(pos.y, minY - 0.5f);
        transform.position = pos;
	}
}
