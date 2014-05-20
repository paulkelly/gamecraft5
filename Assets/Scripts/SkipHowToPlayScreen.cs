using UnityEngine;
using System.Collections;
using InControl;

public class SkipHowToPlayScreen : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InputManager.Setup();
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputManager.Update();
		
		if(InputManager.ActiveDevice.MenuWasPressed)
		{
			Application.LoadLevel(1);
		}
	}
}
