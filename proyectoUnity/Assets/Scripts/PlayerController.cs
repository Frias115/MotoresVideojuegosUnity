using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float velocity = 1f;
    public int health = 3;
    public GameObject bulletPrefab;
    public float bulletVelocity = 1;
    public float bulletPeriod = 0.5f;


    private float nextShootTime = 0.0f;
    private float _velocity;
    private int _health;

	// Use this for initialization
	void Start () {
		_velocity = velocity;
        _health = health;
	}
	
	// Update is called once per frame
	void Update () {
		//Movement 
		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _velocity;

        //Shoot
        if (Input.GetKey(KeyCode.Space))
        {
            if (nextShootTime > bulletPeriod)
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, new Vector3(this.transform.position.x,this.transform.position.y + 0.15f,this.transform.position.z), this.transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletVelocity;
                nextShootTime = 0;
            }
        }
        nextShootTime += Time.deltaTime;
    }

    public void Damage()
    {
        _health--;
        if(_health <= 0)
        {
            //Play animation death
            //Destroy gameObject
            Destroy(gameObject);
            //Show score
            //Return to menu
        }
    }
}
