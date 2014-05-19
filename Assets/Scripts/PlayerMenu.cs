﻿using UnityEngine;
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

		MenuManager.Instance.PlayerReady (playerNumber);
	}

	void Unready()
	{
		playerReady = false;
		renderer.material.color = Color.red;

		MenuManager.Instance.PlayerUnready (playerNumber);
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
				if(inputDevice.Action2)
				{
					Leave ();
				}
			} else
			{
				if(inputDevice.Action2)
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
