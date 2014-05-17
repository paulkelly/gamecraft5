using UnityEngine;
using System.Collections;

public class FanBlower : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D collider)
	{
		transform.parent.GetComponent<BalloonMovement> ().BlowOther (collider.transform);
	}
}
