using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.Enemies)
        {
            other.GetComponent<EnemyController>().Damage();
            Destroy(gameObject);
        } else if(other.gameObject.layer == Layers.Player)
        {
            other.GetComponent<PlayerController>().Damage();
            Destroy(gameObject);
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.Bounds)
        {
            Destroy(gameObject);
        }
    }
}
