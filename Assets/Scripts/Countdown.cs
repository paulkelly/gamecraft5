using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Countdown : MonoBehaviour {

	public static Countdown Instance { get; private set;}

	public Sprite one;
	public Sprite two;
	public Sprite three;

	float count = 3f;

	bool enable = false;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} 
		else if(Instance != this)
		{
			Instance.Start ();
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad (gameObject);
	}

	public void Start()
	{
		Show ();
	}

	public void Show()
	{
		Show (3f);
	}

	public void Show(float t)
	{
		count = t;
		enable = true;
		GetComponent<SpriteRenderer> ().sprite = null;
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void Hide()
	{
		enable = false;
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	void FixedUpdate()
	{
		if (!enable)
		{
			return;
		}
		if(count > 0)
		{
			count -= Time.deltaTime;
			SetTime(count);
		}
		else
		{
			SoundManager.Instance.Beep0();
			enable = false;
			GameMonitor.Instance.Start();
			Hide ();
		}
	}

	public void SetTime(float t)
	{
		if(t > 2)
		{
			if(GetComponent<SpriteRenderer> ().sprite != three)
			{
				GetComponent<SpriteRenderer> ().sprite = three;
				SoundManager.Instance.Beep3();
			}
		}
		else if(t > 1)
		{
			if(GetComponent<SpriteRenderer> ().sprite != two)
			{
				GetComponent<SpriteRenderer> ().sprite = two;
				SoundManager.Instance.Beep2();
			}
		}
		else if(GetComponent<SpriteRenderer> ().sprite != one)
		{
			GetComponent<SpriteRenderer> ().sprite = one;
			SoundManager.Instance.Beep1();
		}
	}
}
