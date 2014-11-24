using UnityEngine;
using System.Collections;

/**
 * Controla o jogador principal 
 * recebendo as entradas do teclado ou joystick 
 * para movimentar o personagem e interagir com o ambiente
 * 
 */

public class ComportamentoJogador : MonoBehaviour {

	public float velocidadeMovimento = 1.54f;
	public float velocidadeMovimentoCorrida = 5f;
	public float velocidadeAnimacao = 1f;

	public float velocidade = 0f;

	private float gravidade = 200f;

	private CharacterController cc;
	private Animation animacao;

	public bool podeExecutarAcao = false;

	public bool latinha = false;
	public bool tcc = false;
	public int dinheiro = 0;
	public int latinhas = 0;

	//private AudioSource emisorDeSom;

	private float tempoEmIdle = 0F;
	private int animacaoIdleDaVez = 3;

	private AudioSource somDePassos;
	private float velocidadeSomPassoAndando = 0.55f;
	private float velocidadeSomPassoCorrendo = 1f;


	void Start()
	{
		cc = GetComponent<CharacterController> ();
		animacao = GetComponent<Animation> ();

		//o jogador acabou ficando sem falas ;)
		//cria um emissor de som para as falas do jogador
		//emisorDeSom = gameObject.AddComponent<AudioSource>();


		//emissor de som dos passos do jogador
		somDePassos = GameAssistente.instance.emissorDePassosDoJogador.GetComponent<AudioSource> ();


		animacao ["acao"].wrapMode = WrapMode.Once;
		animacao ["acao"].layer = 1;

		animacao ["coletarlatinha"].wrapMode = WrapMode.Once;
		animacao ["coletarlatinha"].layer = 1;

		animacao ["inserirlatinha"].wrapMode = WrapMode.Once;
		animacao ["inserirlatinha"].layer = 1;
	}



	void Update() 
	{
		//enquanto o jogador esta falando/ouvindo ele deve esperar a fala terminar
		//entao desabilita os controles para evitar que o jogador sai andado 
		//antes das falas terminarem
		if (GameAssistente.instance.falando) {
			//vai fazer o jogador ficar parado
			velocidade = 0;
		} else {
			//calcula o movimento do personagem e animacoes
			velocidade = Input.GetAxis ("Vertical");
		}

		//determina se o personagem esta andado ou parado
		bool parado = ((velocidade > -0.2) && (velocidade < 0.2));


		if (parado) {
			somDePassos.Stop();

			if (GameAssistente.instance.falando) {
				animacao.CrossFade ("idle03");
			} else {
				//a cada 10seg em idle 
				//sorteia outra animacao 
				if (tempoEmIdle > 10) {
					animacaoIdleDaVez = Random.Range(1, 3);

					//a animacao dois e muito marcante 
					//se ela cair no sorte nao a usa substitui pela 1
					if (animacaoIdleDaVez == 2) {
						animacaoIdleDaVez = 1;
					}

					tempoEmIdle = 0;
				}

				animacao.CrossFade ("idle0" + animacaoIdleDaVez.ToString());
				tempoEmIdle += Time.deltaTime;
			}

			//so pode executar açoes quando esta parado. evita o efeito estranho de animar enqaunto corre.
			podeExecutarAcao = true;
		} else {
			//finaliza qualquer animacao que de idle que tava tocando antes de agir
			tempoEmIdle = 11f;

			podeExecutarAcao = false;
			bool corridaAtivada = (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift) || Input.GetButton ("Correr"));
			bool andandoPraTras = (velocidade < -0.2);

			if (!andandoPraTras && corridaAtivada) {
				velocidade = velocidade * velocidadeMovimentoCorrida;
				velocidadeAnimacao = 0.8f;
				animacao.Play ("corrida");

				//deixa o som de passos correndo com a velocidade normal
				somDePassos.pitch = velocidadeSomPassoCorrendo;

				if (! somDePassos.isPlaying){
					somDePassos.Play();
				}
			} else {
				velocidade = velocidade * velocidadeMovimento;
				velocidadeAnimacao = 1f;

				//controla se a animacao deve ser exibida de tras pra frente
				animacao ["caminhada"].speed = velocidadeAnimacao * (andandoPraTras ? -1 : 1);

				animacao.Play ("caminhada");

				//deixa o som de passos correndo mais lento para 
				//sincronizar com os passos quando esta andando
				somDePassos.pitch = velocidadeSomPassoAndando;

				if (! somDePassos.isPlaying){
					somDePassos.Play();
				}
			}
		} 

		Vector3 movimento = velocidade * transform.TransformDirection (Vector3.forward);
		// aplica força da gravidade
		movimento.y -= gravidade * Time.deltaTime;
		
		cc.Move (movimento * Time.deltaTime);
	}



}
