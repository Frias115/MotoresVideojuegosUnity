using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public float velocity = 1f;
    public int health = 1;
    public GameObject bulletPrefab;
    public float bulletVelocity = 1;
    public float bulletPeriod = 0.25f;
    public GameObject missilePrefab;
    public float missileVelocity = 1;
    public float missilePeriod = 0.25f;
    public float offset = 20;
    public int scoreValue = 100;
    public AudioClip shootSound;


    protected float nextShootTime = 0.0f;
    protected float nextMissileTime = 0.0f;
    protected float _velocity;
    protected int _health;
    protected bool onPlay = false;
    protected GameObject player;
    private float upBound;
    protected int _scoreValue = 100;
    private AudioSource audioSource;
    private SpriteRenderer sr;
    private Transform bulletSpawn, bulletSpawn1, missileSpawn;
    private bool damaged;
    protected float nextDamageTime = 0.0f;
    protected float damagedPeriod = 0.2f;

    // Use this for initialization
    void Start()
    {
        _velocity = velocity;
        _health = health;
        _scoreValue = scoreValue;

        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        bulletSpawn = transform.Find("BulletSpawn");
        Debug.Log(bulletSpawn);
        bulletSpawn1 = transform.Find("BulletSpawn1");
        missileSpawn = transform.Find("MissileSpawn");


        upBound = GameObject.FindGameObjectsWithTag("UpBound")[0].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= upBound - offset)
        {
            //Movement 
            GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.up.x, -transform.up.y) * _velocity;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            //Shoot
            ShootGuided();
            Shoot();
        }

        if (damaged)
        {
            if (nextDamageTime > damagedPeriod)
            {
                damaged = false;
                nextDamageTime = 0;
            }
            nextDamageTime += Time.deltaTime;
        }
        else
        {
            sr.color = Color.white;
        }


    }

    protected void Shoot()
    {
        if (nextShootTime > bulletPeriod)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(Vector2.down));
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletVelocity;
            bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn1.position, Quaternion.Euler(Vector2.down));
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletVelocity;
            nextShootTime = 0;
        }

        nextShootTime += Time.deltaTime;
    }

    protected void ShootGuided()
    {
        if (nextMissileTime > bulletPeriod)
        {
            Vector2 diff = player.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            GameObject missile = GameObject.Instantiate(missilePrefab, missileSpawn.position, Quaternion.Euler(0f, 0f, rot_z - 90));
            missile.GetComponent<Rigidbody2D>().velocity = diff.normalized * missileVelocity;

            audioSource.clip = shootSound;
            audioSource.Play();
            nextMissileTime = 0;
        }

        nextMissileTime += Time.deltaTime;
    }

    public void Damage()
    {
        _health--;
        damaged = true;
        sr.color = Color.red;
        if (_health <= 0)
        {
            HUDManager.score += _scoreValue;
            //Play animation death
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.Player)
        {
            other.GetComponent<PlayerController>().Damage();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.Bounds)
        {
            if (onPlay)
            {
                Destroy(gameObject);
            }

            onPlay = true;
        }
    }
}
