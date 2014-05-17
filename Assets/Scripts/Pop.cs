using UnityEngine;
using System.Collections;

public class Pop : MonoBehaviour {

	public GameMonitor gameMonitor;

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			BalloonMovement balloon = c.gameObject.GetComponent<BalloonMovement>();
			balloon.Pop ();
		}
	}
}
