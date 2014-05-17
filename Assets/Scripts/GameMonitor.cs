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

	public void pop(int playerNum)
	{
		deaths[playerNum-1] = true;
		numDeaths++;
		Debug.Log(playerNum);
		/*if (numDeaths >= numPlayers - 1)
		{
			Debug.Log(playerNum);
		}*/
	}

}
