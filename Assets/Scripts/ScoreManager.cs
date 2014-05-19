using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public Sprite[] number;
	public GameObject[] playerScore;
	
	public int numPlayers = 4;
	public int winsNeeded = 5;
	int[] wins;

	void Update()
	{
		for(int i=0; i<numPlayers; i++)
		{
			playerScore[i].GetComponent<SpriteRenderer>().sprite = number[wins[i]];
		}
		for(int i=numPlayers; i<4; i++)
		{
			playerScore[i].GetComponent<SpriteRenderer>().sprite = number[0];
		}
	}
	
	public void AwardWin(int player)
	{
		wins[player-1]++;
		
		if(wins[player-1] >= winsNeeded)
		{
			GameMonitor.Instance.EndGame (player);
		}
	}
	
	public void Reset(int players)
	{
		numPlayers = players;
		wins = new int[numPlayers];
		for(int i=0; i<numPlayers; i++)
		{
			wins[i] = 0;
		}
	}
}
