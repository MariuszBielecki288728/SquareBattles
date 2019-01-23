using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
	private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
		bar = transform.Find("Bar");
    }
	public void SetSize(float sizeNormalized)
	{
		if (!bar)
			return;
		bar.localScale = new Vector3(1f, sizeNormalized);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
