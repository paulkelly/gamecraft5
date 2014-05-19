using UnityEngine;
using System.Collections;

public class WinnerScript : MonoBehaviour
{

	public Sprite[] number;

	void Start ()
	{
		GameObject.Find("Winner").GetComponent<SpriteRenderer>().sprite = number[GameMonitor.Instance.winner];
	}

	// Update is called once per frame
	void Update () {
			if(Input.GetButtonDown("P" + GameMonitor.Instance.winner + "Start"))
			{
				Application.LoadLevel(0);
			}
	}
}
