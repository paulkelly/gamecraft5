using UnityEngine;
using System.Collections;

public class Pop : MonoBehaviour {

	public GameMonitor gameMonitor;

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			FanController fan = c.gameObject.GetComponent<FanController>();
			gameMonitor.pop(fan.playerNum);
			Destroy(c.gameObject);
		}
	}
}
