using UnityEngine;
using System.Collections;

public class Pop : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(other.gameObject);
		}
	}
}
