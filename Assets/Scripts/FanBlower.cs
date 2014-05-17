using UnityEngine;
using System.Collections;

public class FanBlower : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.GetComponent<BalloonMovement> ())
		{
			transform.parent.GetComponent<BalloonMovement> ().BlowOther (collider.transform);
		}
	}
}
