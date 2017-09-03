using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResolutionScaling : MonoBehaviour {

    [SerializeField]
    private bool maintainWidth;

    private float defaultWidth;
    private float aspect = 0.5625f;
    private Camera cam;

	// Use this for initialization
	void Awake () {
        cam = GetComponent<Camera>();
        defaultWidth = cam.orthographicSize * aspect;
        if (maintainWidth)
            cam.orthographicSize = defaultWidth / cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        if (maintainWidth)
            cam.orthographicSize = defaultWidth / cam.aspect;
	}
}
