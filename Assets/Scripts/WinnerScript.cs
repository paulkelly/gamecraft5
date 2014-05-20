using UnityEngine;
using System.Collections;
using InControl;

public class WinnerScript : MonoBehaviour, GameEvents.GameEventListener
{

	public Sprite[] number;
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}

	void Start ()
	{
		GameEvents.GameEventManager.registerListener(this);
		
		GameObject.Find("Winner").GetComponent<SpriteRenderer>().sprite = number[GameMonitor.Instance.winner];
	}

	// Update is called once per frame
	void GetInput (InputDevice inputDevice) {
		if(inputDevice.MenuWasPressed)
		{
			Application.LoadLevel(1);
		}
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("InputEvent"))
		{
			InputEvent inputEvent = (InputEvent) e;

			GetInput(inputEvent.GetDevice());
			
		}
		
	}
}
