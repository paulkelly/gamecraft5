﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}
}
