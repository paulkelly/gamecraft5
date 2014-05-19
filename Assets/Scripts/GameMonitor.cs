using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMonitor : MonoBehaviour {

	public Sprite[] number;
	public int winner = 1;
	
	public static GameMonitor Instance { get; private set;}

	public GameObject player;
	GameObject[] players;
	bool[] knockedOut;
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
				players[i].GetComponent<BalloonMovement>().froze = false;
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

			Countdown.Instance.Show ();
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
		numDeaths = 0;
		Debug.Log ("Restarting");
		for(int i=0; i<players.Length; i++)
		{
			if(!knockedOut[i])
			{
				Debug.Log ("Player " + i+1 + " gains a point");
				wins[i]++;

				UpdateScore();

				if(wins[i] >= winsNeeded)
				{
					EndGame (i+1);
				}
			}
		}

		//Start next round
		Invoke ("StartTimer", 3f);
	}

	void UpdateScore()
	{
		if (Application.loadedLevel != 1)
			return;

		for(int i=0; i<numPlayers; i++)
		{
			GameObject.Find("Player" + (i+1) + "Score").GetComponent<SpriteRenderer>().sprite = number[wins[i]];
		}
		for(int i=numPlayers; i<4; i++)
		{
			GameObject.Find("Player" + (i+1) + "Score").GetComponent<SpriteRenderer>().sprite = number[0];
		}
	}

	void StartTimer()
	{
		numDeaths = 0;
		Debug.Log ("Starting Timer");
		for(int i=0; i<numPlayers; i++)
		{
			players[i].GetComponent<BalloonMovement>().Reset(GetSpawnPoint());
			knockedOut[i] = false;
		}
		Countdown.Instance.Show ();
	}

	public void EndGame(int winner)
	{
		this.winner = winner;
		Countdown.Instance.Hide ();
		Invoke ("GoToEndScreen", 3f);
	}

	void GoToEndScreen()
	{
		Application.LoadLevel (2);
	}

	public void Reset()
	{
		players = new GameObject[numPlayers];
		wins = new int[numPlayers];
		knockedOut = new bool[numPlayers];
		numDeaths = 0;

		for(int i=0; i<players.Length; i++)
		{
			wins[i] = 0;
			knockedOut[i] = false;
		}

		UpdateScore();
	}

	public void pop(int playerNum)
	{
		numDeaths++;
		knockedOut [playerNum - 1] = true;
		if (numDeaths >= numPlayers - 1)
		{
			Restart ();
		}
	}

}
