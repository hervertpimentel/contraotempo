using UnityEngine;
using System.Collections;

/**
 * Determina o comportamento mais simples em que um personagem esta executando
 * uma atividade e so para quando o personamgem chega muito perto dele para interagir.
 * 
 */

public class ComportamentoPadrao : MonoBehaviour {

	private Animation animacao;

	public string animacaoPadrao;
	public string animacaoInteracao;
	public float distanciaMinima = 1f;

	private AudioSource emisorDeSom;

	public string textoFalaSemTcc;
	public AudioClip audioFalaSemTcc;

	public string textoFalaComTcc;
	public AudioClip audioFalaComTcc;


	private bool falou = false;

	void Start()
	{

		animacao = GetComponent<Animation> ();
		emisorDeSom = gameObject.AddComponent<AudioSource>();
	}
	
	void Update() 
	{
		if (Vector3.Distance (transform.position, GameAssistente.instance.player.transform.position) <= distanciaMinima) 
		{		
			//faz o persoangem andar de frente para o ponto de destino
			transform.LookAt (GameAssistente.instance.player.transform.position);
			animacao.CrossFade (animacaoInteracao);

		} else {
			animacao.CrossFade (animacaoPadrao);

			//quando o jogador se afasta o NPC 
			//o NPC esquece que falou com o jogador
			//assim ele pode repetir a fala quando o jogador voltar
			falou = false;
		}
	}


	//executado quando o player interage com o NPC
	public void acao()
	{	
		if (! falou)
		{
			falou = true;
			
			//verifica se o jogador ja tem o tcc na mao
			if (GameAssistente.instance.itensDoJogador.tcc) {
				emisorDeSom.PlayOneShot (audioFalaComTcc);
				GameAssistente.instance.exibirLegenda (textoFalaComTcc, audioFalaComTcc.length);
			} else {
				emisorDeSom.PlayOneShot (audioFalaSemTcc);
				GameAssistente.instance.exibirLegenda (textoFalaSemTcc, audioFalaSemTcc.length);
			}
		}
	}
}
