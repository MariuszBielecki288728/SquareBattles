using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed;
	public int type;
    public GameObject bullet;
	public GameObject healthBarPrefab;
	public GameObject explosion;
	private HealthBarController barController;
    private Rigidbody2D rb2d;
    public GameObject target = null;
    public bool isGunPositioned = false;
	public int health;
	private int maxHealth;
	private int bulletCounter = 0;
	private bool isQuitting = false;
	public GameObject plansza;
	public uint reward;

	// Start is called before the first frame update
	void Start()
    {
		maxHealth = health;
		GameObject bar = Instantiate(healthBarPrefab, this.transform);
		bar.transform.localPosition = this.tag == "Ally" ? new Vector3(-0.7f, 0) : new Vector3(0.7f, 0);
		barController = bar.GetComponent<HealthBarController>();
	}

    // Update is called once per frame
    void Update()
    {
		if (barController)
		{
			Debug.Log("cyyk");
			barController.SetSize(health / (float)maxHealth);
		}

		if (health <= 0)
		{
			Destroy(this.gameObject);
			return;
		}
        if (isGunPositioned == false)
        {
            Vector3? pos = FindEnemyPosition();
            if (pos != null)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
        
    }
    void OnEnable()
	{
		
		Move();
		if (type==0)
			InvokeRepeating("Fire", 0.5f, 0.8f);
		else if (type==1)
			InvokeRepeating("Fire1", 0.5f, 0.4f);
		else if (type==2)
			InvokeRepeating("Fire2", 0.5f, 0.2f);
		plansza = GameObject.Find("plansza");

	}
	void OnApplicationQuit()
	{
		isQuitting = true;
	}
	private void OnDestroy()
	{
		if (explosion && !isQuitting)
		{
			Instantiate(explosion, this.transform.position, Quaternion.identity);
		}
		if (this.tag == "Enemy")
		{
			plansza.GetComponent<GameController>().money += reward;
		}

	}

	private void Move()
	{
		rb2d = GetComponent<Rigidbody2D>();
		if (this.tag == "Ally")
		{
			rb2d.velocity = new Vector2(0, speed);
		}
		else
		{
			rb2d.velocity = new Vector2(0, -speed);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "bullet" && collision.gameObject.GetComponent<BulletController>().allyTag != this.tag)
		{
			health -= collision.GetComponent<BulletController>().damage;
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
                this.transform.GetChild(0).up = dir.normalized;
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
            GameObject bul = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
            bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
			bul.GetComponent<BulletController>().allyTag = this.tag;

		}
		if (!target)
		{
			isGunPositioned = false;
		}
        
    }
	void Fire1()
	{
		
		if (isGunPositioned)
		{
			GameObject bul = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
			if (bulletCounter % 2 == 0)
			{
				bul.transform.position += new Vector3(0.15f, 0, 0);
			}
			else
			{
				bul.transform.position += new Vector3(-0.15f, 0, 0);
			}
			bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
			bul.GetComponent<BulletController>().allyTag = this.tag;
			bulletCounter++;
			bulletCounter %= 2;
		}
		if (!target)
		{
			isGunPositioned = false;
		}

	}

	void Fire2()
	{
		if (isGunPositioned)
		{
			GameObject bul = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
			if (bulletCounter % 3 == 0)
			{
				bul.transform.position += new Vector3(0.04f, 0, 0);
			}
			else if (bulletCounter % 3 == 2)
			{
				bul.transform.position += new Vector3(-0.04f, 0, 0);
			}
			bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
			bul.GetComponent<BulletController>().allyTag = this.tag;
			bulletCounter++;
			bulletCounter %= 3;
		}
		if (!target)
		{
			isGunPositioned = false;
		}

	}
}
