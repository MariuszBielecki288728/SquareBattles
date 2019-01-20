using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public string allyTag;
	public int damage;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != allyTag)
		{
			Destroy(this.gameObject);
		}
		
	}
}
