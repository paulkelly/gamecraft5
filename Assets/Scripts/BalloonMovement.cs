﻿using UnityEngine;
using System.Collections;

public class BalloonMovement : MonoBehaviour
{
	public float fanRadius = 0.4f;
	public float fanForce = 15;
	public float maxSpeed = 6;

	Vector2 fanDirection = Vector3.down;
	Vector3 fanRotation = Vector3.down;

	float fanPower = 0f;
	float getBlownStrength = 1f;

	GameObject fan;

	// Use this for initialization
	void Start ()
	{
		fan = gameObject.transform.FindChild ("Fan").gameObject;
		fanDirection = fanRotation * fanRadius;
	}

	void FixedUpdate ()
	{
		if (fanPower > 0)
		{
			transform.rigidbody2D.AddForce(fanDirection.normalized * -fanForce * fanPower);
		}

		if (transform.rigidbody2D.velocity.magnitude > maxSpeed)
		{
			transform.rigidbody2D.velocity = transform.rigidbody2D.velocity.normalized * maxSpeed;
		}

		fan.transform.position = new Vector3(transform.position.x + fanDirection.x, transform.position.y + fanDirection.y, fan.transform.position.z);
	}

	public void BlowOther(Transform other)
	{
		if(other.GetComponent<BalloonMovement>() != null && fan != null)
		{
			other.GetComponent<BalloonMovement>().GetBlown (fan.transform, fanPower);
		}
	}

	void GetBlown(Transform blower, float otherFanPower)
	{
		if (otherFanPower > 0)
		{
			Vector2 pushDirection = new Vector2(transform.position.x - blower.position.x, transform.position.y - blower.position.y);

			float magnitude = 13 - pushDirection.magnitude;

			magnitude = magnitude * magnitude;

			transform.rigidbody2D.AddForce(pushDirection.normalized * magnitude * otherFanPower * getBlownStrength);
		}
	}

	public void SetFanPower(float power)
	{
		fanPower = power;
	}

	public void Move(float h, float v)
	{
		Vector2 direction = new Vector2 (h, v);

		if (direction.magnitude > 0.6f)
		{
			fanDirection = direction.normalized * fanRadius;
			fanRotation = new Vector3( 0, 0, Mathf.Atan2(v, h) * 180 / Mathf.PI);
			fan.transform.rotation = Quaternion.Euler (fanRotation);
		}
	}

	public void Pop()
	{
		int playerNum = GetComponent<FanController> ().playerNum;
		GameMonitor.Instance.pop (playerNum);
		Destroy (gameObject);
	}
}
