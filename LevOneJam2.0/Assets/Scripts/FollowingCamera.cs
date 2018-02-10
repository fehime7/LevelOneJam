using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	void Start () {
		offset = transform.transform.position - target.position; //vector from camera to player
	}

	void FixedUpdate() {
		if (target)
		{
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
}