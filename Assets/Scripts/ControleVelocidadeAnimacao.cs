using UnityEngine;
using System.Collections;

public class ControleVelocidadeAnimacao : MonoBehaviour {

	public string nomeDaAnimacao = "";
	public float velocidadeDaAnimacao = 1f;

	void Start () {
		if (nomeDaAnimacao != "") {
			gameObject.animation [nomeDaAnimacao].speed = velocidadeDaAnimacao;
		}
	}
}
