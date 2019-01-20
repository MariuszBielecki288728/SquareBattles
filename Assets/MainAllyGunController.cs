using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAllyGunController : MonoBehaviour
{
	public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// convert mouse position into world coordinates
		Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// get direction you want to point at
		Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;

		// set vector of transform directly
		transform.up = direction;

		if (Input.GetMouseButtonDown(0))
		{
			Fire();
		}
	}

	private void Fire()
	{
		GameObject bul = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
		bul.GetComponent<Rigidbody2D>().velocity = transform.GetChild(0).up * 10;
		bul.GetComponent<BulletController>().allyTag = this.tag;
	}
}
