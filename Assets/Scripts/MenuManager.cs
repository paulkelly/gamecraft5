using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public static MenuManager Instance { get; private set; }

	public static int maxPlayers = 4;
	bool[] playerJoined = new bool[maxPlayers];
	bool[] playerReady = new bool[maxPlayers];

	bool allReady = false;
	public static float startCountdown = 2;
	float startCountdownRem = startCountdown;

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

	void Start()
	{
		for (int i=0; i<maxPlayers; i++)
		{
			playerJoined [i] = false;
			playerReady [i] = false;
		}
	}

	public void PlayerJoin(int number)
	{
		playerJoined [number - 1] = true;
	}

	public void PlayerLeave(int number)
	{
		playerJoined [number - 1] = false;;
	}

	public void PlayerReady(int number)
	{
		playerReady [number - 1] = true;
	}

	public void PlayerUnready(int number)
	{
		playerReady [number - 1] = false;
	}

	public int GetNextAvailabileCharacter()
	{
		return 1;
	}

	public void FixedUpdate()
	{
		int numPlayersJoined = 0;

		for (int i=0; i<maxPlayers; i++)
		{
			if(playerJoined[i])
			{
				numPlayersJoined++;
			}
		}

		if (numPlayersJoined < 2)
		{
			return;
		}

		int numPlayersReady = 0;
		
		for (int i=0; i<maxPlayers; i++)
		{
			if(playerReady[i])
			{
				numPlayersReady++;
			}
		}

		if (numPlayersReady >= numPlayersJoined)
		{
			allReady = true;
			if(allReady && startCountdownRem <= 0)
			{
				StartGame(numPlayersReady);
			}
			else
			{
				startCountdownRem -= Time.deltaTime;
			}
		}
		else
		{
			allReady = false;
			startCountdownRem = startCountdown;
		}
	}

	void StartGame(int players)
	{
		SetPlayerMappings();
	
		for (int i=0; i<maxPlayers; i++)
		{
			playerJoined [i] = false;
			playerReady [i] = false;
		}
		allReady = false;
		startCountdownRem = startCountdown;

		GameMonitor.Instance.numPlayers = players;
		Application.LoadLevel (2);

	}
	
	public void SetPlayerMappings()
	{
		int[] playerMappings = new int[maxPlayers];
		
		int j=0;
		for(int i=0; i<maxPlayers; i++)
		{
			if(playerReady[i])
			{
				playerMappings[j] = i+1;
				j++;
			}
		}
		
		GameMonitor.Instance.SetPlayerMappings(playerMappings);
	}
}
