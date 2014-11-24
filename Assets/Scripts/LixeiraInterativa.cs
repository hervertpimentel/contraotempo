using UnityEngine;
using System.Collections;

public class LixeiraInterativa : MonoBehaviour {

	public string animacao = "acao";

	private string animacaoObjeto;
	private AudioSource emissorDeSom;

	void Start()
	{
		animacaoObjeto = GameAssistente.pegaNomeDaAnimacao (0, gameObject);
		emissorDeSom = gameObject.AddComponent<AudioSource> ();
	}


	public void acao()
	{
		//toca som do elemento sendo jogado na lixeira
		GameAssistente.instance.tocarSom (emissorDeSom, GameAssistente.instance.somJogarLatinhaNoLixo);

		//faz a acao do player...
		GameObject player = GameAssistente.instance.player;
		player.animation.Play(animacao);		

		Animation animador = gameObject.animation;

		animador[animacaoObjeto].speed = 1f;
		animador[animacaoObjeto].wrapMode = WrapMode.Once;
		animador.Play(animacaoObjeto);

		//incrementa o HUD pra dar o feedback do item completado
		if (GameAssistente.instance.itensDoJogador.latinha) {
			GameAssistente.instance.ocultarLatinhaDaMaoDoJogador ();
			GameAssistente.instance.itensDoJogador.latinhas++; 
		}
	}
}
