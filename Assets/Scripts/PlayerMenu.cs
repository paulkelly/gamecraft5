using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour {

	public int playerNumber = 1;

	public Sprite PressStart;
	public Sprite PressA;

	bool playerJoined = false;
	bool playerReady = false;

	void Start()
	{
		renderer.material.color = Color.white;
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
	void Update ()
	{
		if(Input.GetButtonDown("P" + playerNumber + "Start"))
		{
			Join ();
		}

		if (playerJoined)
		{
			if(!playerReady)
			{
				if(Input.GetButtonDown("P" + playerNumber + "A"))
				{
					Ready ();
				}
				if(Input.GetButtonDown("P" + playerNumber + "B"))
				{
					Leave ();
				}
			} else
			{
				if(Input.GetButtonDown("P" + playerNumber + "B"))
				{
					Unready ();
				}
			}
		}
	}
}
