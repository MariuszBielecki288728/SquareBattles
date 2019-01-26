using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCameraController : MonoBehaviour
{
	public float speed = 0.1F;
	public float threshodDistance;
	public float cameraMoveSpeed;
	public float boundary;

	private Vector3 dragOrigin; //Where are we moving?
	private Vector3 clickOrigin = Vector3.zero; //Where are we starting?
	private Vector3 basePos = Vector3.zero; //Where should the camera be initially?
	private int? currentTouch;
	private Vector2 begTouchPos;
	

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began && currentTouch.Equals(null))
			{
				currentTouch = touch.fingerId;
				begTouchPos = touch.position;
				
			}
			if (currentTouch != null)
			{
				float deltaPos = touch.position.x - begTouchPos.x;
				Debug.Log(deltaPos.ToString());
				if (Mathf.Abs(deltaPos) > threshodDistance)
				{
					this.transform.position += new Vector3(0, Mathf.Sign(deltaPos) * cameraMoveSpeed);
					this.transform.position = new Vector3(
						0, 
						deltaPos < 0 ? 
							Mathf.Max(this.transform.position.y, -boundary) : 
							Mathf.Min(this.transform.position.y, boundary), -10);
				}
			}
			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				currentTouch = null;
			}

		}


		/*  if (Input.GetMouseButton(0))
		{
			if (clickOrigin == Vector3.zero)
			{
				clickOrigin = Input.mousePosition;
				basePos = transform.position;
			}
			dragOrigin = Input.mousePosition;
		}

		if (!Input.GetMouseButton(0))
		{
			clickOrigin = Vector3.zero;
			return;
		}

		transform.position = new Vector3(0, basePos.y + ((clickOrigin.x - dragOrigin.x) * .05f), -10);*/
	} 
}
