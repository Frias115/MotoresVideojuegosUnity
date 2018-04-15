using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float velocity = 1f;

	private float _velocity;
	private GameObject _background;

	// Use this for initialization
	void Start () {
		_velocity = velocity;
		_background = GameObject.Find("Background");
	}
	
	// Update is called once per frame
	void Update () {
		//Movement 
		//Debug.Log(_background.transform.localScale.x);
		//if(this.transform.position.x < _background.transform.position.x  &&  this.transform.position.x > -_background.transform.position.x)
		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _velocity;
	}
}
