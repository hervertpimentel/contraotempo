using UnityEngine;
using System.Collections;

/**
 * Determina o comportamento do seguranca
 * bloqueando a passagem quando o jogador chega muito perto
 * sem ter cumprido os objetivos
 * ou liberando a passagem e carregado a tela de boas vindas 
 * quando todo o objetivo foi concluido
 */

public class CompartamentoSeguranca : MonoBehaviour {

	private double distancia;
	private Animation animacao;
	private AudioSource emisorDeSom;

	public string textoFalaSemTcc;
	public AudioClip audioFalaSemTcc;
	
	public string textoFalaComTcc;
	public AudioClip audioFalaComTcc;

	private bool falou = false;

	//usado para mostrar um final especial
	//quando o jogador finaliza o jogo 
	//em menos de 5 min
	private float tempoJogando = 0f;

	void Start () 
	{
		animacao = GetComponent<Animation> ();
		emisorDeSom = gameObject.AddComponent<AudioSource>();
	}


	void Update () 
	{
		//contabiliza o tempo que o jogador esta jogando o jogo
		tempoJogando += Time.deltaTime;

		animacao.CrossFade ("vigiando");

		//calcula a distancia entre o jogador e o seguranca
		distancia = Vector3.Distance(transform.position, GameAssistente.instance.player.transform.position);

		//se o jogador esta muito perto entao o seguranca age de acordo 
		if (distancia < 1.2) {
			//faz o seguranca ficar de frente com o jogador
			transform.LookAt (GameAssistente.instance.player.transform.position);

			//verifica se o jogador ja tem o tcc na mao
			if (GameAssistente.instance.itensDoJogador.tcc) {
				if (! falou) {
					GameAssistente.instance.tocarSom (emisorDeSom, audioFalaComTcc);
					GameAssistente.instance.exibirLegenda (textoFalaComTcc, audioFalaComTcc.length);
					falou = true;
				}

				//ganhou o jogo!!!
				StartCoroutine (playAnimacaoGameWin ());
			} else {
				if (! falou) {
					GameAssistente.instance.tocarSom (emisorDeSom, audioFalaSemTcc);
					GameAssistente.instance.exibirLegenda (textoFalaSemTcc, audioFalaSemTcc.length);
					falou = true;
				}

				//o jogador ainda nao tem o que precisa para passar
				//o seguranca o manda parar
				animacao.CrossFade ("pare");
			}
		} else {
			//quando o jogador se distancia 
			//o NPC esquece que ja falou com o jogador
			//assim ele repete o que tive pra falar 
			//assim que o jogador chega perto novamente
			falou = false;
		}
	}



	IEnumerator playAnimacaoGameWin()
	{
		//libera a passagem para o jogador
		animacao.CrossFade("liberado");
		yield return new WaitForSeconds (animacao["liberado"].length);

		//escurece a camera
		Camera.main.SendMessage("fadeOut");
		yield return new WaitForSeconds (1.7F);
		
		float cincoMinutos = (5 * 60);

		//carregar a tela de vitoria 
		//de acordo com o tempo do jogador
		//o final especial ou o final normal
		if (tempoJogando <= cincoMinutos) {
			Application.LoadLevel ("GameWinEspecial");
		} else {
			Application.LoadLevel ("GameWin");
		}
	}	
	
	
}
