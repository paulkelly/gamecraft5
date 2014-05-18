using UnityEngine;
using System.Collections;

public class GameMonitor : MonoBehaviour {

	public static GameMonitor Instance { get; private set;}

	public GameObject player;
	GameObject[] players;
	int[] wins;
	public int numPlayers = 4;
	public int numDeaths = 0;

	public int winsNeeded = 5;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Instance.Reset();
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	
	public void Start()
	{
		if (Application.loadedLevel != 1)
						return;

		players[0] = (GameObject)Instantiate(player, new Vector3(-1f, 1f, 0f), Quaternion.identity);
		players[0].GetComponent<FanController>().playerNum = 1;

		if (numPlayers > 1)
		{
			players[1] = (GameObject)Instantiate(player, new Vector3(1f, 1f, 0f), Quaternion.identity);
			players[1].GetComponent<FanController>().playerNum = 2;
		}

		if (numPlayers > 2)
		{
			players[2] = (GameObject)Instantiate(player, new Vector3(-1f, -1f, 0f), Quaternion.identity);
			players[2].GetComponent<FanController>().playerNum = 3;
		}

		if (numPlayers > 3)
		{
			players[3] = (GameObject)Instantiate(player, new Vector3(1f, -1f, 0f), Quaternion.identity);
			players[3].GetComponent<FanController>().playerNum = 4;
		}
	}

	public void Restart()
	{
		for(int i=0; i<players.Length; i++)
		{
			if(players[i] != null)
			{
				wins[i]++;
				Destroy(players[i]);

				if(wins[i] >= winsNeeded)
				{
					EndGame (i+1);
				}
				else
				{
					//Start next round
					Countdown.Instance.Show ();
				}
			}
		}
	}

	public void EndGame(int winner)
	{
		Debug.Log("Player " + winner + "won.");
		Application.LoadLevel (0);
	}

	public void Reset()
	{
		players = new GameObject[numPlayers];
		wins = new int[numPlayers];

		for(int i=0; i<players.Length; i++)
		{
			wins[i] = 0;
		}
	}

	public void pop(int playerNum)
	{
		players[playerNum-1] = null;
		numDeaths++;
		if (numDeaths >= numPlayers - 1)
		{
			Restart ();
		}
	}

}
