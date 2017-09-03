﻿using System.Collections;
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
    [SerializeField]
    private PlayerController player;
    private float minY;

	// Use this for initialization
	void Start () {
        Vector3 pos = transform.position;
        pos.y = target.position.y + offset;
        transform.position = pos;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(transform.position.y, target.position.y + offset, (1 / followRate) * Time.deltaTime);
        if (pos.y > minY)
            minY = pos.y;
        pos.y = Mathf.Max(pos.y, minY);
        transform.position = pos;
	}
}
