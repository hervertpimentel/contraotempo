using UnityEngine;
using System.Collections;

public class GameOverCoracaoBatendo : MonoBehaviour {

	private float tempo = 0;
	private AudioSource emissorDeSom ;
	// Use this for initialization
	void Start () {
		emissorDeSom = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;

		//depois de alguns segundos no game over
		//toca o coracao batendo pra deixar mais tenso
		if (tempo > 7) {
			if (! emissorDeSom.isPlaying) {
				emissorDeSom.Play();
			}
		}
	}
}
