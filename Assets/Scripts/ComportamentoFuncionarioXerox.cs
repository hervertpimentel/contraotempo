using UnityEngine;
using System.Collections;

public class ComportamentoFuncionarioXerox : MonoBehaviour {

	
	private Animation animacao;
	
	public string animacaoPadrao;
	public string animacaoInteracao;

	public float distanciaMinima = 1f;
	
	private AudioSource emisorDeSom;
	
	public string textoFalaNenhumaLatinha;
	public AudioClip audioFalaNenhumaLatinha;

	public string textoFalaSemLatinhasSuficientes;
	public AudioClip audioFalaSemLatinhasSuficientes;

	public string textoFalaEntregaTcc;
	public AudioClip audioFalaEntregaTcc;
	
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
			if (GameAssistente.instance.itensDoJogador.latinhas >= 5) {
				emisorDeSom.PlayOneShot (audioFalaEntregaTcc);
				GameAssistente.instance.exibirLegenda (textoFalaEntregaTcc, audioFalaEntregaTcc.length);

				//oculta alguma latinha se ele a tiver na mao
				GameAssistente.instance.ocultarLatinhaDaMaoDoJogador ();

				//faz a acao do player...
				GameObject player = GameAssistente.instance.player;
				player.animation.Play("acao");		

				//motra o documento na mao do jogador
				GameAssistente.instance.mostrarTccNaMaoDoJogador();
			} else {

				//se ja coletou alguma latinha mas nao o suficiente
				//manda buscar mais latas pra completar o jogo
				if (GameAssistente.instance.itensDoJogador.latinhas >= 1) 
				{
					emisorDeSom.PlayOneShot (audioFalaSemLatinhasSuficientes);
					GameAssistente.instance.exibirLegenda (textoFalaSemLatinhasSuficientes, audioFalaSemLatinhasSuficientes.length);
				} else {
					emisorDeSom.PlayOneShot (audioFalaNenhumaLatinha);
					GameAssistente.instance.exibirLegenda (textoFalaNenhumaLatinha, audioFalaNenhumaLatinha.length);
				}
			}
		}
	}
}
