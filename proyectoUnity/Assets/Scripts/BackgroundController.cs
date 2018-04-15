using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
	
	public float scrollVelocity = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Transform>().position += -new Vector3(0f, scrollVelocity, 0f) * Time.fixedDeltaTime;
	}
}
