using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BalloonMovement))]
public class FanController : MonoBehaviour {

	public int playerNum = 1;

	private BalloonMovement balloon;

	void Start()
	{
		balloon = GetComponent<BalloonMovement>();
	}
	
	void FixedUpdate ()
	{
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		float v = CrossPlatformInput.GetAxis("Vertical");
		#else
		float h = Input.GetAxis("P" + playerNum + "Horizontal");
		float v = Input.GetAxis("P" + playerNum + "Vertical");
		#endif

		balloon.Move (h, v);

	}
}
