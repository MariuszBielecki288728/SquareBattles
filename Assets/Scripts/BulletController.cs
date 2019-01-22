using UnityEngine;

public class BulletController : MonoBehaviour
{
	public string allyTag;
	public int damage;
	public uint destroyAfter;
	public GameObject explosion;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != allyTag)
		{
			Destroy(this.gameObject);
		}

	}

	private void Awake()
	{
		Destroy(this.gameObject, destroyAfter);
	}

	private void OnDestroy()
	{
		if (explosion)
		{
			Instantiate(explosion, this.transform.position, Quaternion.identity);
		}

	}
}
