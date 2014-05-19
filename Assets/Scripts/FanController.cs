using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(BalloonMovement))]
public class FanController : MonoBehaviour, GameEvents.GameEventListener
{

	public int playerNum = 1;

	private BalloonMovement balloon;

	void Start()
	{
		GameEvents.GameEventManager.registerListener(this);
		
		balloon = GetComponent<BalloonMovement>();
	}
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}

	void Update()
	{

	}
	
	void GetInput (InputDevice inputDevice)
	{
		float h = inputDevice.LeftStickX;
		float v = inputDevice.LeftStickY;

		float f = inputDevice.RightTrigger;
		if(f == 0 && inputDevice.Action1)
		{
			f = 1;
		}

		balloon.Move (h, v);

		balloon.SetFanPower(f);

	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("InputEvent"))
		{
			InputEvent inputEvent = (InputEvent) e;
			if(inputEvent.GetPlayerNumber() == playerNum)
			{
				GetInput(inputEvent.GetDevice());
			}
		}
		
	}
}
