using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainEnemyGunController : MonoBehaviour
{
	public GameObject bullet;
	public GameObject target = null;
	public bool isGunPositioned = false;
	public int health;

	public Text enemyHealtText;
	private AudioManager audioManager;
	// Start is called before the first frame update
	void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
		InvokeRepeating("Fire", 0.5f, 1.5f);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "bullet" && collision.gameObject.GetComponent<BulletController>().allyTag != this.tag)
		{
			health -= collision.GetComponent<BulletController>().damage;
		}

	}

	// Update is called once per frame
	void Update()
	{
		enemyHealtText.text = "Enemy HP: " + health.ToString();
		if (health <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		SceneManager.LoadScene("VictoryScreen");
	}

	void Fire()
	{
		if (isGunPositioned == false)
		{
			Vector3? pos = FindEnemyPosition();
		}

		if (isGunPositioned)
		{
			GameObject bul = Instantiate(bullet, transform.GetChild(1).position, transform.GetChild(1).rotation);
			bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(1).up * 10;
			bul.GetComponent<BulletController>().allyTag = this.tag;
		}
		if (!target)
		{
			isGunPositioned = false;
		}
		

	}
	Vector3? FindEnemyPosition()
	{
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 18);
		foreach (Collider2D col in hitColliders)
		{
			if (col.tag != "bullet" && col.tag != "Map" && this.tag != col.tag)
			{
				Vector2 dir = col.transform.position - this.transform.position;
				transform.up = dir.normalized;
				isGunPositioned = true;
				target = col.gameObject;
				return col.transform.position;

			}
		}
		return null;

	}
}