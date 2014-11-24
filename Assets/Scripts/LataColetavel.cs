using UnityEngine;
using System.Collections;

public class LataColetavel : MonoBehaviour {

	public string animacao = "acao";

	void Start()
	{
	}
	
	void acao()
	{
		//se o jogador ja estiver com uma latinha nao mao entao NAO coleta outra
		if (GameAssistente.instance.itensDoJogador.latinha) 
		{
			return;
		}
		
		//Game over!!!
		StartCoroutine(playAnimacaoPlayer());
	}	

	private IEnumerator playAnimacaoPlayer(){
		//faz a acao do player...
		GameAssistente.instance.player.animation.Play(animacao);		

		//espera ate que a animacao complete (braco esticado) para sumir com a lata
		//se nao a lata aparecia na mao antes do usuario abaixar pra pegar
		yield return new WaitForSeconds (GameAssistente.instance.player.animation[animacao].length);

		//desabilita a latinha que esta sendo coletada
		GameAssistente.instance.ativarOuDesativarObjeto (this.gameObject.transform.parent.gameObject, false);
		
		//faz a latinha aparecer na mao do player
		GameAssistente.instance.mostrarLatinhaNaMaoDoJogador();
		
		//toca o power up de que coletou um item
		AudioSource emissorDeSom = GameAssistente.instance.latinhaDaMaoDoJogador.GetComponent<AudioSource> ();
		GameAssistente.instance.tocarSom (emissorDeSom, GameAssistente.instance.somColetaLatinha);
	}

}
