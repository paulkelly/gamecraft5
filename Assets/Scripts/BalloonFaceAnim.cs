using UnityEngine;
using System.Collections;

public class BalloonFaceAnim : MonoBehaviour {

	private bool attack;
	private bool death = false;
	private bool reset = false;

	private float random;
	private float timer;

	private Animator anim;

	// Use this for initialization
	void Start () {
	
		attack = false;
		anim = this.GetComponent<Animator>();
		random = Random.Range (0f,1f);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(anim.GetBool("Attack") == false)
			timer += Time.deltaTime;
		else timer = 0;

		if((timer - random) > 1f){
		   	anim.SetTrigger("Blink");
			random = Random.Range (0f,1.5f);
			timer = 0f;
		}

		if(attack){
			if(anim.GetBool("Attack") != true)  
				anim.SetBool("Attack",true);
		}
		else{
			if(anim.GetBool("Attack") != false)
				anim.SetBool("Attack",false);
		}

		if(death){
			anim.SetTrigger("Death");
			Debug.Log ("Done Death");
			death = false;
		}
		if(reset){
			anim.SetTrigger("Reset");
			Debug.Log("Done Reset");
			reset = false;
		}

	}

	public void Attack(bool b){

		attack = b;

	}

	public void Pop(){ death = true;Debug.Log ("POP");}
	public void Reset() { reset = true;}


}
