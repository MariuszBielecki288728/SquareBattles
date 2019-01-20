using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed;
    public GameObject bullet;
    private Rigidbody2D rb2d;
    public GameObject target = null;
    public bool isGunPositioned = false;
	private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
		InvokeRepeating("Fire", 0.5f, 0.5f);

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
}
