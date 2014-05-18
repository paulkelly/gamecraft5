using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMonitor : MonoBehaviour {

	public static GameMonitor Instance { get; private set;}

	public GameObject player;
	GameObject[] players;
	public GameObject[] spawners;
	List<GameObject> usedSpawners = new List<GameObject>();

	public Color[] playerColors;

	int[] wins;
	public int numPlayers = 4;
	public int numDeaths = 0;

	public int winsNeeded = 5;

	bool started = false;

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
		if(started)
		{
			for(int i=0; i<numPlayers; i++)
			{
				players[i].GetComponent<BalloonMovement>().Reset(GetSpawnPoint());
			}
		}
		else
		{
			for(int i=0; i<numPlayers; i++)
			{
				players[i] = (GameObject)Instantiate(player, GetSpawnPoint(), Quaternion.identity);
				players[i].GetComponent<FanController>().playerNum = i+1;
				players[i].GetComponent<BalloonMovement>().SetColor(playerColors[i]);
			}
		}

		started = true;
	}

	Vector3 GetSpawnPoint()
	{
		int i = Random.Range (0, spawners.Length);
		int max = 20;
		int j = 0;
		while(usedSpawners.Contains(spawners[i]) && j<max)
		{
			i = Random.Range (0, spawners.Length);
			j++;
		}
		usedSpawners.Add (spawners[i]);
		return spawners [i].transform.position;
	}

	public void Restart()
	{
		usedSpawners.Clear ();
		for(int i=0; i<players.Length; i++)
		{
			if(!players[i].GetComponent<BalloonMovement>().popped)
			{
				wins[i]++;

				if(wins[i] >= winsNeeded)
				{
					EndGame (i+1);
				}
			}
		}

		//Start next round
		Invoke ("StartTimer", 1f);
	}

	void StartTimer()
	{
		Countdown.Instance.Show ();
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
		numDeaths = 0;

		for(int i=0; i<players.Length; i++)
		{
			wins[i] = 0;
		}
	}

	public void pop(int playerNum)
	{
		numDeaths++;
		if (numDeaths >= numPlayers - 1)
		{
			Restart ();
		}
	}

}
