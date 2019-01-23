using UnityEngine;

public class BulletController : MonoBehaviour
{
	public string allyTag;
	public int damage;
	public uint destroyAfter;
	public bool isEnergy = false;
	public GameObject explosion;
	private bool isQuitting = false;
	private string enemyTag;
	private void Start()
	{
		if (allyTag == "Ally")
			enemyTag = "Enemy";
		else if (allyTag == "Enemy")
			enemyTag = "Ally";
		
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != allyTag && !isEnergy || collision.tag == enemyTag)
		{
			Destroy(this.gameObject);
		}

	}

	private void Awake()
	{
		Destroy(this.gameObject, destroyAfter);
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

	}
}
