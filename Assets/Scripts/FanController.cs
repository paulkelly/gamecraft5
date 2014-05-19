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

	void Update()
	{

	}
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("P" + playerNum + "Horizontal");
		float v = Input.GetAxis("P" + playerNum + "Vertical");

		float f = Input.GetAxis("P" + playerNum + "Trigger");
		if(f == 0 && Input.GetButton("P" + playerNum + "A"))
		{
			f = 1;
		}

		balloon.Move (h, v);

		balloon.SetFanPower(f);

	}
}
