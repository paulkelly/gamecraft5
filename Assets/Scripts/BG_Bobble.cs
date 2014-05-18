using UnityEngine;
using System.Collections;

public class BG_Bobble : MonoBehaviour {

	public GameObject[] BG;
	public GameObject[] FG;

	public float[] BG_Pos;
	public float[] FG_Pos;

	public float speed = 0.1f;
	public float max = 1f;

	// Use this for initialization
	void Start () {
	
		BG_Pos = new float[BG.Length];
		FG_Pos = new float[FG.Length];

		for(int i = 0; i < BG.Length; i++){

			BG_Pos[i] = i*speed;


		}
		for(int i = 0; i < FG.Length; i++){
		FG_Pos[i] = Random.Range(0,Mathf.PI*2)-Mathf.PI;
		}
	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i < BG.Length; i++){


				BG_Pos[i] += Time.deltaTime*0.5f;					
				

			BG[i].transform.position += new Vector3(0,Mathf.Sin(Mathf.PI*BG_Pos[i])/1000f,0);
		

		}
		for(int j = 0; j < FG.Length; j++){
			FG_Pos[j] += Time.deltaTime*0.25f;
			FG[j].transform.position += new Vector3(0,Mathf.Sin(Mathf.PI*FG_Pos[j])/500f,0);

		}


	


	}
}
