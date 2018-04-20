using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /*TO DO:
     * Animaciones
     * Musica
     * Efectos
     * UI
     * Menu
     * Menu pausa
     * Boss
     * Score max y multiplicador
     * Niveles de dificultad
     * MEMORIA
     */

	public float velocity = 1f;
    public int health = 3;
    public GameObject bulletPrefab;
    public float bulletVelocity = 1;
    public float bulletPeriod = 0.5f;


    private float nextShootTime = 0.0f;
    private float _velocity;
    private int _health;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float upBound, downBound, leftBound, rightBound;
    private float spriteOffSetOnX, spriteOffSetOnY;
    private float velocityOnX, velocityOnY;
    private int score = 0;

	// Use this for initialization
	void Start () {
		_velocity = velocity;
        _health = health;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spriteOffSetOnX = sr.size.x;
        spriteOffSetOnY = sr.size.y;
        upBound = GameObject.FindGameObjectsWithTag("UpBound")[0].transform.position.y;
        downBound = GameObject.FindGameObjectsWithTag("DownBound")[0].transform.position.y;
        leftBound = GameObject.FindGameObjectsWithTag("LeftBound")[0].transform.position.x;
        rightBound = GameObject.FindGameObjectsWithTag("RightBound")[0].transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        //Movement 
        //X
        if(transform.position.x - spriteOffSetOnX <= leftBound)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                velocityOnX = 0;
            }
            else
            {
                velocityOnX = Input.GetAxisRaw("Horizontal");
            }
        }
        else if (transform.position.x + spriteOffSetOnX >= rightBound)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                velocityOnX = 0;
            }
            else
            {
                velocityOnX = Input.GetAxisRaw("Horizontal");
            }
        }
        else
        {
            velocityOnX = Input.GetAxisRaw("Horizontal");
        }

        //Y
        if (transform.position.y - spriteOffSetOnY <= downBound)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                velocityOnY = 0;
            }
            else
            {
                velocityOnY = Input.GetAxisRaw("Vertical");
            }
        }
        else if (transform.position.y + spriteOffSetOnY >= upBound)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                velocityOnY = 0;
            }
            else
            {
                velocityOnY = Input.GetAxisRaw("Vertical");
            }
        }
        else
        {
            velocityOnY = Input.GetAxisRaw("Vertical");
        }

        rb.velocity = new Vector2(velocityOnX, velocityOnY) * _velocity;

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

    public void addScore(int quantity)
    {
        score += quantity;
    }
}
