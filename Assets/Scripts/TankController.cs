using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed;
    public int maxHealth;
    public int type;
    public uint reward;
    public GameObject bullet;
    public GameObject healthBarPrefab;
    public GameObject explosion;
    public GameObject plansza;
    public GameObject target = null;
    public bool isGunPositioned = false;
    private int health;
    private float healthBar;
    private float speed_;
    private int bulletCounter = 0;
    private bool isQuitting = false;

	private Transform tankTower;
    private HealthBarController barController;
    private Rigidbody2D rb2d;
    private AudioManager audioManager;


    void OnEnable()
    {
        GameObject bar = Instantiate(healthBarPrefab, this.transform);
        bar.transform.localPosition = this.tag == "Ally" ? new Vector3(-0.7f, 0) : new Vector3(0.7f, 0);
        barController = bar.GetComponent<HealthBarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		tankTower = transform.GetChild(0);
        healthBar = health = maxHealth;
        speed_ = (this.tag == "Ally") ? speed : -speed;
        StartAI();
    }

    void StartAI()
    {
        Move();
        if (type == 0)
            InvokeRepeating("Fire", 0.5f, 0.8f);
        else if (type == 1)
            InvokeRepeating("Fire1", 0.5f, 0.4f);
        else if (type == 2)
            InvokeRepeating("Fire2", 0.5f, 0.2f);
        plansza = GameObject.Find("plansza");
    }
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        healthBar = Mathf.Lerp(healthBar, health, Time.deltaTime * 6);;
        barController.SetSize(healthBar / (float)maxHealth);
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (explosion && !isQuitting)
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
        }
        if (this.tag == "Enemy")
        {
            if (!plansza)
                return;
            plansza.GetComponent<GameController>().money += reward;
        }
    }
    private void Move()
    {
        rb2d.velocity = new Vector2(0, speed_);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet" && collision.gameObject.GetComponent<BulletController>().allyTag != this.tag)
        {
            health -= collision.GetComponent<BulletController>().damage;

            if (health <= 0)
            {
                audioManager.Play("TankExplosion");
                Destroy(this.gameObject);
            }
        }

    }

    Vector3? FindEnemyPosition()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 12);
        foreach (Collider2D col in hitColliders)
        {
            if (col.tag != "bullet" && col.tag != "Map" && this.tag != col.tag)
            {
                Debug.Log(col.tag);
                Vector2 dir = col.transform.position - this.transform.position;
                tankTower.up = dir.normalized;

                isGunPositioned = true;
                target = col.gameObject;
                return col.transform.position;
            }
        }
        Move();
        return null;
    }
    void Fire()
    {
        if (isGunPositioned)
        {
			UpdateGunPosition();
            GameObject bul = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
            bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
            bul.GetComponent<BulletController>().allyTag = this.tag;

        }
        if (!target)
        {
            isGunPositioned = false;
            Vector3? pos = FindEnemyPosition();
            if (pos != null)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
    }
    void Fire1()
    {
        if (isGunPositioned)
        {
			UpdateGunPosition();
			GameObject bul;
            if (bulletCounter % 2 == 0)
            {
                bul = Instantiate(bullet, transform.GetChild(0).TransformPoint(new Vector3(0.15f, 0, 0)), transform.GetChild(0).rotation);
            }
            else
            {
                bul = Instantiate(bullet, transform.GetChild(0).TransformPoint(new Vector3(-0.15f, 0, 0)), transform.GetChild(0).rotation);

            }
            bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
            bul.GetComponent<BulletController>().allyTag = this.tag;
            bulletCounter++;
            bulletCounter %= 2;
        }
        if (!target)
        {
            isGunPositioned = false;
            Vector3? pos = FindEnemyPosition();
            if (pos != null)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
    }

    void Fire2()
    {
        if (isGunPositioned)
        {
			UpdateGunPosition();
			GameObject bul;
            if (bulletCounter % 3 == 0) //TODO this is really ugly
            {
                bul = Instantiate(bullet, transform.GetChild(0).GetChild(1).TransformPoint(new Vector3(0.05f, 0)), transform.GetChild(0).GetChild(1).rotation);

            }
            else if (bulletCounter % 3 == 2)
            {
                bul = Instantiate(bullet, transform.GetChild(0).GetChild(1).TransformPoint(new Vector3(-0.05f, 0)), transform.GetChild(0).GetChild(1).rotation);
            }
            else
            {
                bul = Instantiate(bullet, transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(1).rotation);

            }
            bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
            bul.GetComponent<BulletController>().allyTag = this.tag;
            bulletCounter++;
            bulletCounter %= 3;
        }
        if (!target)
        {
            isGunPositioned = false;
            Vector3? pos = FindEnemyPosition();
            if (pos != null)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
    }
	private void UpdateGunPosition()
	{
		if (target)
		{
			Vector2 dir = target.transform.position - this.transform.position;
			this.transform.GetChild(0).up = dir.normalized;
		}

	}
}
