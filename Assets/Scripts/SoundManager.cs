﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	public static SoundManager Instance { get; private set;}

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
	
	public float minPitch = 0.5f;
	public float maxPitch = 1.5f;
	
	public AudioClip[] pop;
	public float minPopVolume = 0.25f;
	public float maxPopVolume = 1f;
	
	public AudioClip[] cheer;
	public float minCheerVolume = 0.25f;
	public float maxCheerVolume = 1f;
	
	private void PlayRandomSound(AudioClip clip, float pitch, float volume)
	{
		audio.pitch = pitch;
		audio.volume = volume;
		audio.PlayOneShot(clip);
	}
	
	public void PlayPop()
	{
		PlayRandomSound (pop[Random.Range (0, pop.Length)], 
		                 Random.Range (minPitch, maxPitch), 
		                 Random.Range (minPopVolume, maxPopVolume));
	}
	
	public void PlayCheer()
	{
		PlayRandomSound (cheer[Random.Range (0, cheer.Length)], 
		                 Random.Range (minPitch, maxPitch), 
		                 Random.Range (minCheerVolume, maxCheerVolume));
	}

}
