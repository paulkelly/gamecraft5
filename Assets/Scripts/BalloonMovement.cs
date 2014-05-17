using UnityEngine;
using System.Collections;

public class BalloonMovement : MonoBehaviour
{
	float fanRadius = 0.4f;
	Vector2 fanDirection = Vector3.up;
	Vector3 fanRotation = Vector3.down;

	GameObject fan;

	// Use this for initialization
	void Start ()
	{
		fan = gameObject.transform.FindChild ("Fan").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Move(float h, float v)
	{
		//invert y axis
		v = v * -1;

		fanDirection = new Vector2 (h, v);
		fanRotation = new Vector3( 0, 0, Mathf.Atan2(v, h) * 180 / Mathf.PI);

		if (fanDirection.magnitude > 0.6f)
		{
			fanDirection = fanDirection.normalized * fanRadius;
			fan.transform.position = new Vector3(fanDirection.x, fanDirection.y, fan.transform.position.z);
			fan.transform.rotation = Quaternion.Euler (fanRotation);
		}
	}
}
