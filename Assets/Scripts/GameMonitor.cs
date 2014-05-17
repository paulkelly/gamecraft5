using UnityEngine;
using System.Collections;

public class GameMonitor : MonoBehaviour {

	public static GameMonitor Instance { get; private set;}

	public GameObject player;
	GameObject[] players;
	bool[] deaths;
	public int numPlayers = 4;
	public int numDeaths = 0;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	
	public void Start()
	{
		if (Application.loadedLevel != 1)
						return;
		players = new GameObject[numPlayers];
		deaths = new bool[numPlayers];

		players[0] = (GameObject)Instantiate(player, new Vector3(-1f, 1f, 0f), Quaternion.identity);
		players[0].GetComponent<FanController>().playerNum = 1;
		deaths[0] = false;

		if (numPlayers > 1)
		{
			players[1] = (GameObject)Instantiate(player, new Vector3(1f, 1f, 0f), Quaternion.identity);
			players[1].GetComponent<FanController>().playerNum = 2;
			deaths[1] = false;
		}

		if (numPlayers > 2)
		{
			players[2] = (GameObject)Instantiate(player, new Vector3(-1f, -1f, 0f), Quaternion.identity);
			players[2].GetComponent<FanController>().playerNum = 3;
			deaths[2] = false;
		}

		if (numPlayers > 3)
		{
			players[3] = (GameObject)Instantiate(player, new Vector3(1f, -1f, 0f), Quaternion.identity);
			players[3].GetComponent<FanController>().playerNum = 4;
			deaths[3] = false;
		}
	}

	public void Restart()
	{
		Countdown.Instance.Show ();

		for(int i=0; i<players.Length; i++)
		{
			if(players[i] != null)
			{
				Destroy(players[i]);
			}
		}
	}

	public void Reset()
	{
	}

	public void pop(int playerNum)
	{
		deaths[playerNum-1] = true;
		numDeaths++;
		if (numDeaths >= numPlayers - 1)
		{
			Restart ();
		}
	}

}
