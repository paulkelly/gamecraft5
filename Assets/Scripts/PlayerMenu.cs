using UnityEngine;
using System.Collections;
using InControl;

public class PlayerMenu : MonoBehaviour, GameEvents.GameEventListener
{

	public int playerNumber = 1;

	public Sprite PressStart;
	public Sprite PressA;

	bool playerJoined = false;
	bool playerReady = false;

	void Start()
	{		
		GameEvents.GameEventManager.registerListener(this);
		
		renderer.material.color = Color.white;
	}
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}

	void Join()
	{
		playerJoined = true;
		playerReady = false;
		renderer.material.color = Color.red;
		transform.FindChild("PlayerSelected").GetComponent<SpriteRenderer> ().sprite = PressA;

		MenuManager.Instance.PlayerJoin (playerNumber);
	}

	void Leave()
	{
		playerJoined = false;
		renderer.material.color = Color.white;
		transform.FindChild("PlayerSelected").GetComponent<SpriteRenderer> ().sprite = PressStart;

		MenuManager.Instance.PlayerLeave (playerNumber);
	}

	void Ready()
	{
		playerReady = true;
		renderer.material.color = Color.green;
		transform.FindChild("PlayerSelected").GetComponent<SpriteRenderer> ().enabled = false;

		MenuManager.Instance.PlayerReady (playerNumber);
		
		int[] defaultMappings = new int[4];
		for(int i=0; i<4; i++)
		{
			defaultMappings[i] = i+1;
		}
		GameMonitor.Instance.SetPlayerMappings(defaultMappings);

		GameMonitor.Instance.SpawnPlayer(playerNumber, false);
	}

	void Unready()
	{
		playerReady = false;
		renderer.material.color = Color.red;
		transform.FindChild("PlayerSelected").GetComponent<SpriteRenderer> ().enabled = true;

		MenuManager.Instance.PlayerUnready (playerNumber);
		GameMonitor.Instance.PopPlayer(playerNumber);
	}
	
	// Update is called once per frame
	void GetInput (InputDevice inputDevice)
	{	
		if(inputDevice.MenuWasPressed)
		{
			Join ();
		}

		if (playerJoined)
		{
			if(!playerReady)
			{
				if(inputDevice.Action1)
				{
					Ready ();
				}
				if(inputDevice.Action2.WasPressed)
				{
					Leave ();
				}
			} else
			{
				if(inputDevice.Action2.WasPressed)
				{
					Unready ();
				}
			}
		}
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("InputEvent"))
		{
			InputEvent inputEvent = (InputEvent) e;
			if(inputEvent.GetPlayerNumber() == playerNumber)
			{
				GetInput(inputEvent.GetDevice());
			}
		}
		
	}
	
	
}
