using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 *  Define o comportamento de um personagem que pode andar pelo cenario 
 *  usando pontos de destinos pre definidos
 * 
 */ 

public class ComportamentoAndarilho : MonoBehaviour {

	public float velocidade = 0.8f;

	public List<GameObject> lugares = new List<GameObject> ();
	public GameObject destino;

	private CharacterController cc;
	private int contador = 0;
	private bool voltando = false;

	private Animation animacao;

	private AudioSource emisorDeSom;

	public string textoFalaSemTcc;
	public AudioClip audioFalaSemTcc;
	
	public string textoFalaComTcc;
	public AudioClip audioFalaComTcc;

	private bool falou = false;

	void Start () 
	{
		destino = lugares[0];
		animacao = GetComponent<Animation> ();
		cc = transform.GetComponent<CharacterController> ();

		emisorDeSom = gameObject.AddComponent<AudioSource>();
	}
	

	void Update () 
	{
		//se chegou ja a um destino escolhe outro destino pra ir
		if (Vector3.Distance(transform.position, destino.transform.position) <= 1.5) {

			//quanto o personagem ja andou por todos os pontos do caminho ele volta 
			//pelo mesmo caminho que fez.
			if (voltando){
				destino = lugares[--contador];
			} else {
				destino = lugares[++contador];
			}

			//se chegou no ultimo ponto do caminho
			//comeca a voltar 
			if (contador >= lugares.Count -1){
				voltando = true;
			}

			//se voltou ate o primeiro ponto entao re-inicia 
			//todo o caminho
			if (contador <= 0){
				voltando = false;
			}
		}

		if (Vector3.Distance (transform.position, GameAssistente.instance.player.transform.position) <= 1) {		
			//faz o persoangem andar de frente para o ponto de destino
			transform.LookAt(GameAssistente.instance.player.transform.position);
			animacao.CrossFade ("idle03");


		} else {

			//aponta o personagem na direcao do ponto de destino
			Quaternion rotacao = Quaternion.LookRotation(destino.transform.position - transform.position);

			//faz o personagem rotacionar suavemente na direcao do destino
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * 6);

			//movimenta o personagem
			animacao.CrossFade ("caminhada");
			transform.Translate(0,0,velocidade * Time.deltaTime);

			//aplica força da gravidade
			Vector3 movimento = velocidade * transform.TransformDirection (Vector3.down);
			cc.Move (movimento * Time.deltaTime);

			//quando o NPC volta a andar uns 3m de distancia do jogador
			//ele esquece que falou com o jogador
			//assim ele pode repetir a fala quando o jogador voltar pra falar com ele
			if (Vector3.Distance (transform.position, GameAssistente.instance.player.transform.position) > 3) {
				falou = false;
			}
		}
	}


	public void acao()
	{
		if (! falou)
		{
			falou = true;
			
			//verifica se o jogador ja tem o tcc na mao
			if (GameAssistente.instance.itensDoJogador.tcc) {
				emisorDeSom.PlayOneShot(audioFalaComTcc);
				GameAssistente.instance.exibirLegenda(textoFalaComTcc, audioFalaComTcc.length);
			} else {
				emisorDeSom.PlayOneShot(audioFalaSemTcc);
				GameAssistente.instance.exibirLegenda(textoFalaSemTcc, audioFalaSemTcc.length);
			}
		}
	}
}