using UnityEngine;
using System.Collections;

public class GameMonitor : MonoBehaviour {

	public GameObject player;
	GameObject[] players;
	bool[] deaths;
	public int numPlayers = 4;
	public int numDeaths = 0;

	// Use this for initialization
	void Start ()
	{
		players = new GameObject[numPlayers];
		deaths = new bool[numPlayers];
		for (int i = 0; i < numPlayers; i++)
		{
			players[i] = (GameObject)Instantiate(player);
			deaths[i] = false;
		}
	}

	public void pop(int playerNum)
	{
		deaths[playerNum] = true;
		numDeaths++;
		Debug.Log(playerNum);
		/*if (numDeaths >= numPlayers - 1)
		{
			Debug.Log(playerNum);
		}*/
	}

}
