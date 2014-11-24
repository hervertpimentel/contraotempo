using UnityEngine;
using System.Collections;

public class IndicadorDeAcao : MonoBehaviour 
{	
	public Texture2D iconeAcao;
	public string texto;
	public float maximoTempoVisivel = 0;

	private string nomeSkin = "IndicadorAcao";

	public int iconeOffSetInferior = 85;
	public int textoOffSetInferior = 130;
	
	private int alturaTexto = 40;
	
	
	private bool exibir = false;
	private float tempoVisivel = 0f;

	private float tempoTranscorridoUltimaAcao = 0f;
	public float tempoMinimoRepeticao = 2f;
	
	
	public GameObject objeto;

	void OnTriggerEnter(Collider outro)
	{
		exibir = (outro.gameObject.tag == "Player");
		tempoVisivel = 0;
		tempoTranscorridoUltimaAcao = tempoMinimoRepeticao;
	}
	
	void OnTriggerExit(Collider outro)
	{
		exibir = false;
	}
	
	void Update()
	{


		if (exibir)
		{
			//para de exibir os indicadores de acao
			//quando começam os dialogos
			//pra nao sobrescrever as legendas
			if (GameAssistente.instance.falando){
				exibir = false;
			}

			tempoTranscorridoUltimaAcao += Time.deltaTime;
			
			if (Input.GetButtonUp("Acao") && (tempoTranscorridoUltimaAcao >= tempoMinimoRepeticao))
			{
				tempoTranscorridoUltimaAcao = 0;

				//faz a acao do objeto 'tocado' pelo player
				objeto.SendMessage("acao");
			}
		}
	}
	
	
	void OnGUI()
	{
		//aplica a aparerencia do texto modificado
		GUI.skin = GameAssistente.instance.gameGuiSkin;
		
		if (exibir)
		{
			tempoVisivel += Time.deltaTime;
			
			if ((maximoTempoVisivel > 0) && (tempoVisivel > maximoTempoVisivel))
			{
				exibir = false;	
				return;
			}
			
			Rect posicaoTexto = new Rect (0, Screen.height - textoOffSetInferior, Screen.width, alturaTexto);
			
			if (iconeAcao != null)
			{
				Rect posicaoIcone = posicaoTexto;
				posicaoIcone.width = iconeAcao.width;
				posicaoIcone.height = iconeAcao.height;
				posicaoIcone.y -= iconeOffSetInferior;
				posicaoIcone.x = (Screen.width - iconeAcao.width) / 2;
				
				GUI.DrawTexture(posicaoIcone, iconeAcao);
			}
			
			GUI.Label (posicaoTexto, texto, nomeSkin);		
		}
	}
	
}
