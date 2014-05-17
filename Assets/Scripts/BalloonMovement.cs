using UnityEngine;
using System.Collections;

public class BalloonMovement : MonoBehaviour
{
	float fanRadius = 0.2f;
	Quaternion fanAngle = Quaternion.identity;

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

	}
}
