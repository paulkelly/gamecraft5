using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{

	public static InputHandler Instance { get; private set;}
	
	int i = 0;
	List<InputDevice> device;

	// Use this for initialization
	void Start ()
	{
	
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Destroy(gameObject);
		}

		//InputManager.EnableXInput = true;
		InputManager.Setup();
		
		device = new List<InputDevice>();
		
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputManager.Update();
		
		if(InputManager.ActiveDevice.MenuWasPressed)
		{
			bool newDevice = true;
			foreach(InputDevice inputDevice in device)
			{
				if(inputDevice == null || InputManager.ActiveDevice == null || inputDevice.Equals(InputManager.ActiveDevice))
				{
					newDevice = false;
				}
			}
			
			if(newDevice && i < 4)
			{
				Debug.Log("New device registered");
				device.Add(InputManager.ActiveDevice);
				i++;	
			}
		}
		
		int j=1;
		foreach(InputDevice inputDevice in device)
		{
			InputEvent e = new InputEvent(inputDevice, j);
			GameEvents.GameEventManager.post(e);
			j++;
		}
		
	}
}
