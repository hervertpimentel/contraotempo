using UnityEngine;
using System.Collections;

public class Roleta : MonoBehaviour {

	public GameObject roleta;
	
	void OnTriggerEnter(Collider outro)
	{
		if (outro.gameObject.tag == "Player") {
			string animacao = GameAssistente.pegaNomeDaAnimacao(0, roleta);
			Animation animation = roleta.gameObject.animation;

			gameObject.GetComponent<AudioSource>().Play();

			animation.Play(animacao);
		}
	}
}
