using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMonitor : MonoBehaviour {

	public int winner = 1;
	
	public static GameMonitor Instance { get; private set;}

	public GameObject player;
	GameObject[] players = new GameObject[4];
	bool[] knockedOut = new bool[4];
	public GameObject[] spawners;
	List<GameObject> usedSpawners = new List<GameObject>();

	public Color[] playerColors;

	public int numPlayers = 4;
	public int numDeaths = 0;
	
	int[] playerMappings = new int[4];

	bool started = false;
	bool gameOver = false;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			started = false;
			Instance.Start();
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	
	public void SetPlayerMappings(int[] newMappings)
	{
		playerMappings = newMappings;
	}
	
	public void Start()
	{

		if (Application.loadedLevel != 2)
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
			Reset();
			
			for(int i=0; i<numPlayers; i++)
			{
				SpawnPlayer(i+1, true);
			}
		}

		started = true;
	}
	
	public void SpawnPlayer(int number, bool freeze)
	{
		if(players[number-1] == null)
		{
			players[number-1] = (GameObject)Instantiate(player, GetSpawnPoint(), Quaternion.identity);
			players[number-1].GetComponent<FanController>().playerNum = number;
			players[number-1].GetComponent<BalloonMovement>().SetColor(playerColors[number-1]);
			players[number-1].GetComponent<FanController>().controllerNum = playerMappings[number-1];
		}
		else
		{
			players[number-1].GetComponent<FanController>().controllerNum = playerMappings[number-1];
			players[number-1].GetComponent<BalloonMovement>().Reset(GetSpawnPoint());
			knockedOut[number-1] = false;
		}
		
		if(freeze)
		{
			players[number-1].GetComponent<BalloonMovement>().froze = true;
		}
		else
		{
			players[number-1].GetComponent<BalloonMovement>().froze = false;
		}
	}
	
	public void PopPlayer(int number)
	{
		players[number-1].GetComponent<BalloonMovement>().Pop();
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
		if(j >= max)
		{
			usedSpawners.Clear ();
		}
		usedSpawners.Add (spawners[i]);
		return spawners [i].transform.position;
	}

	public void Restart()
	{
		usedSpawners.Clear ();
		numDeaths = 0;
		for(int i=0; i<numPlayers; i++)
		{
			if(!knockedOut[i])
			{
				GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AwardWin(i+1);
			}
		}

		//Start next round
		Invoke ("StartTimer", 3f);
	}

	void StartTimer()
	{
		if(gameOver)
		{	
			return;
		}
		numDeaths = 0;
		for(int i=0; i<numPlayers; i++)
		{
			SpawnPlayer(i+1, true);
		}
		Countdown.Instance.Show ();
	}

	public void EndGame(int winner)
	{
		gameOver = true;
		this.winner = winner;
		Countdown.Instance.Hide ();
		Invoke ("GoToEndScreen", 4f);
	}

	void GoToEndScreen()
	{
		Countdown.Instance.Hide ();
		Application.LoadLevel (3);
	}

	public void Reset()
	{
		gameOver = false;
		GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Reset(numPlayers);
		numDeaths = 0;

		for(int i=0; i<players.Length; i++)
		{
			knockedOut[i] = false;
		}

	}

	public void pop(int playerNum)
	{
		if (Application.loadedLevel != 2)
			return;
			
		numDeaths++;
		knockedOut [playerNum - 1] = true;
		Celebration();
		
		if (numDeaths >= numPlayers - 1)
		{
			Restart ();
		}
	}
	
	public void Celebration()
	{
		for(int i=0; i<players.Length; i++)
		{
			if(players[i] != null && players[i].GetComponent<BalloonFaceAnim>())
			{
				if(!players[i].GetComponent<BalloonMovement>().popped)
				{
					players[i].GetComponent<BalloonFaceAnim>().Celebrate();
				}
			}
		}
	}

}
