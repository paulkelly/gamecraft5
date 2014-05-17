using UnityEngine;
using System.Collections;

public class Pop : MonoBehaviour {

	public GameMonitor gameMonitor;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			FanController fan = other.gameObject.GetComponent<FanController>();
			gameMonitor.pop(fan.playerNum);
			Destroy(other.gameObject);
		}
	}
}
