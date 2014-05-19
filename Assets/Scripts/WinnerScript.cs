using UnityEngine;
using System.Collections;

public class WinnerScript : MonoBehaviour {

	void Start ()
	{
		GameObject.Find("Winner").GetComponent<SpriteRenderer>().sprite = GameMonitor.Instance.number[GameMonitor.Instance.winner];
	}

	// Update is called once per frame
	void Update () {
			if(Input.GetButtonDown("P" + GameMonitor.Instance.winner + "Start"))
			{
				Application.LoadLevel(0);
			}
	}
}
