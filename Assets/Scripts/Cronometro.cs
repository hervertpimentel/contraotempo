using UnityEngine;
using System.Collections;


public class Cronometro : MonoBehaviour {
	
	//quantos minutos o jogador vai ter pra descobrir o puzzle
	public int tempoEmMinutos = 5;
	
	//indica a posicao da tela em que o cronometro vai aparecer	
	public float textoOffSetLateral = 5.0f;
	public float textoOffSetSuperior = 10.0f;

	private float alturaTexto = 197f;
	private float larguraTexto = 256f;
	
	//a configuracao da fonte e tamanho utulizado para exibir o cronometro
	private string nomeSkin = "IndicadorCronometro";
	
	//utilizado internamento pelo cronometro pra saber quando tempo o jogador ainda tem pra resolver o puzzle
	private float tempoRestante = 0F;	
	
	//animacao executada quando tempo acabar e o jogador nao tiver concluido o puzzle
	public string nomeAnimacaoGameOver = "gameover"; 

	public GameObject emissorBatimentoCardico;
	private AudioSource somCoracao;

	void Start () {
		tempoRestante = tempoEmMinutos * 60; 
		somCoracao =  emissorBatimentoCardico.GetComponent<AudioSource> ();
	}
	
	void Update () {
		tempoRestante -= Time.deltaTime;

		//quando estiver faltando menos de 60seg 
		//toca uma batida de coracao
		//pra dar mais adrenalina
		if ((tempoRestante <= 60f) && (! somCoracao.isPlaying)) {
			somCoracao.volume = 0.6f;
			somCoracao.Play();
		}

		//acelera o som de batimento cardiaco 
		//quanto ta perto do fim do tempo do jogo
		//da mais adrenalina pro jogador
		if ((tempoRestante <= 30f) && (somCoracao.isPlaying)) {
			somCoracao.volume = 0.85f;
			somCoracao.pitch = 1.34f;
		}
		
		if (tempoRestante <= 0)
		{
			tempoRestante = 0;
						
			//Game over!!!
			StartCoroutine(playAnimacaoGameOverPlayer());
		}
	}
	

	void OnGUI(){
		//aplica a aparerencia do texto modificado
		GUI.skin = GameAssistente.instance.gameGuiSkin;
		
		Rect posicaoCronometro = new Rect ((Screen.width - larguraTexto) - textoOffSetLateral, textoOffSetSuperior, larguraTexto, alturaTexto);		
		GUI.Label (posicaoCronometro, formatarTempo(tempoRestante), nomeSkin);		
	}
	
	
	private string formatarTempo(float tempo)
	{
		int minutos = (int) tempo / 60 ;	
	    int segundos = (int) tempo % 60;
	    return minutos.ToString() + ":" + segundos.ToString("00");
	}
	
	
	IEnumerator playAnimacaoGameOverPlayer()
	{
		GameObject player = GameAssistente.instance.player;		

		GameAssistente.instance.desabilitarControleDoJogador();

		player.animation.Play(nomeAnimacaoGameOver);
		yield return new WaitForSeconds (player.animation[nomeAnimacaoGameOver].length);

		Camera.main.SendMessage("fadeOut");
		yield return new WaitForSeconds (1.7F);
		
		//carregar game over 
		Application.LoadLevel("GameOver");
	}	
	
}
