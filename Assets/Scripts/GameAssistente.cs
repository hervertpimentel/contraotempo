using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Essa classe mantem cache de algumas informacoes
 * para acesso rapido pelos outros objetos 
 */


public class GameAssistente : MonoBehaviour 
{
	public static GameAssistente instance ;
	
	private string nomeSkinLegenda = "IndicadorLegenda";
	private string nomeSkinContador = "IndicadorContador";

	public GameObject player;	
	public CharacterController ccJogador;
	public ComportamentoJogador itensDoJogador;
	public GUISkin gameGuiSkin; 

	public GameObject latinhaDaMaoDoJogador;
	public GameObject tccDaMaoDoJogador;

	public AudioClip somPortaAbrindo;
	public AudioClip somPortaFechando;

	public AudioClip somColetaLatinha;
	public AudioClip somJogarLatinhaNoLixo;
	public AudioClip somItemCompletado;

	public AudioClip somRoleta;

	public GameObject emissorDePassosDoJogador;


	private Queue<Legenda> legendas = new Queue<Legenda> ();
	private Legenda legenda;	
	private float tempoVisivel = 0F;

	public bool falando = false;


	public float offsetLateralContador = 222f;
	public float offsetSuperiorContador = 30f;


	public List<GameObject> locaisDeLatas = new List<GameObject> ();

	public Texture2D backgroundTarjaDeDialogo;




	void Start () {
		instance = gameObject.GetComponent<GameAssistente>();
		itensDoJogador = player.GetComponent<ComportamentoJogador>();

		ccJogador = player.GetComponent<CharacterController> ();


		//desativa umas latas e deixa so outras
		for (int i = 0; i < 5; i++) {
			ativarOuDesativarObjeto(locaisDeLatas[Random.Range(0, 10)], false);
		}
	}
	
	public static string pegaNomeDaAnimacao(int indice, GameObject mesh)
	{
		int i = 0;
		foreach (AnimationState a in mesh.animation)
		{
			if (indice == i)
			{
				return a.name;				
			}
			
			i++;
		}
		
		return null;
	}



	public void mostrarLatinhaNaMaoDoJogador()
	{
		if (latinhaDaMaoDoJogador != null) 
		{
			ativarOuDesativarObjeto(latinhaDaMaoDoJogador, true);
			itensDoJogador.latinha = true;
		}
	}
	
	public void ocultarLatinhaDaMaoDoJogador()
	{
		if (latinhaDaMaoDoJogador != null) 
		{
			ativarOuDesativarObjeto(latinhaDaMaoDoJogador, false);
			itensDoJogador.latinha = false;
		}
	}


	public void mostrarTccNaMaoDoJogador()
	{
		ativarOuDesativarObjeto(tccDaMaoDoJogador, true);
		itensDoJogador.tcc = true;
	}
	

	
	public void ativarOuDesativarObjeto(GameObject g, bool a) {
		g.SetActive(a);
		
		foreach (Transform child in g.transform) {
			ativarOuDesativarObjeto(child.gameObject, a);
		}
	}




	private IEnumerator tocarComDelay( AudioSource audioSource, AudioClip audio, float delay ){
		if( audioSource == null )
			yield break;
		
		yield return new WaitForSeconds( delay );
		
		audioSource.PlayOneShot (audio);
		audioSource.volume = 0.6f;
	}


	public void tocarSomComDelay(AudioSource audioSource, AudioClip audio, float delay )
	{
		if (! falando) {
			StartCoroutine (tocarComDelay (audioSource, audio, delay));
		}
	}

	public void tocarSom(AudioSource audioSource, AudioClip audio )
	{
		if (! falando) {
			audioSource.PlayOneShot (audio);
		}
	}


	public void exibirLegenda(string texto, float segundos)
	{
		if (! falando) {
			Legenda leg = new Legenda ();
			leg.texto = texto;
			leg.tempoVisivel = segundos + 5;

			legendas.Enqueue (leg);
		}
	}


	public void desabilitarControleDoJogador(){
		//desabilita os controles do usuario...
		if (ccJogador.enabled) {
			ccJogador.enabled = false;
		}
	}

	public void habilitarControleDoJogador(){
		//re habilita os controles do usuario...
		if (! ccJogador.enabled) {
			ccJogador.enabled = true;
		}
	}



	void Update()
	{
		bool temLegendaPraExibir = (legendas.Count > 0);

		if ((falando == false) && (temLegendaPraExibir)) {
			//puxa a proxima legenda da lista
			//e remove a legenda e exibicao da lista das que ainda deverao ser exibidas
			legenda = legendas.Dequeue();

			//configura as coisas necessarias a comecar a exibir a legenda
			tempoVisivel = 0;
			falando = true;
		}

		if ( Input.GetButtonUp("Cancelar") )
		{
			Application.LoadLevel("Menu");
		}	
	}


	void OnGUI()
	{
		//aplica a aparerencia do texto modificado
		GUI.skin = GameAssistente.instance.gameGuiSkin;
	
		if (falando) 
		{
			tempoVisivel += Time.deltaTime;
			
			if ((legenda.tempoVisivel > 0) && (tempoVisivel > legenda.tempoVisivel)) {
				falando = false;	
				return;
			}
			
			Rect posicaoTexto = new Rect (0, Screen.height - 210f, Screen.width, 100f);
			GUI.Label (posicaoTexto, legenda.texto, nomeSkinLegenda);		
		}



		//mostra o contador de latinhas coletadas
		//quando o jogado nao esta em dialogo
		if (!falando) {
			Rect posicaoContador = new Rect ((Screen.width - 97.11f) - offsetLateralContador, offsetSuperiorContador, 97.11f, 79.2f); //97.11 x 79.2  = tamanho do icone de latinhas
			GUI.Label (posicaoContador, itensDoJogador.latinhas.ToString (), nomeSkinContador);		
		} else {
			//mostra tarjas indicativas de dialogo
			Rect posicaoTarjaSuperior = new Rect (0, 0, Screen.width, 100.0f);
			Rect posicaoTarjaInferior = new Rect (0, Screen.height - 100.0f, Screen.width, 100.0f);

			GUI.DrawTexture(posicaoTarjaSuperior, backgroundTarjaDeDialogo);
			GUI.DrawTexture(posicaoTarjaInferior, backgroundTarjaDeDialogo);
		}
	}
}



