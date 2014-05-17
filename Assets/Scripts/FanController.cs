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
		float h = CrossPlatformInput.GetAxis("P" + playerNum + "Horizontal");
		float v = CrossPlatformInput.GetAxis("P" + playerNum + "Vertical");

		float f = CrossPlatformInput.GetAxis("P" + playerNum + "Trigger");
		#else
		float h = Input.GetAxis("P" + playerNum + "Horizontal");
		float v = Input.GetAxis("P" + playerNum + "Vertical");

		float f = Input.GetAxis("P" + playerNum + "Trigger");
		#endif

		balloon.Move (h, v);

		balloon.SetFanPower(f);

	}
}
