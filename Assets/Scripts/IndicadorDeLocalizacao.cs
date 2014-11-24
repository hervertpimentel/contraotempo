using UnityEngine;
using System.Collections;

public class IndicadorDeLocalizacao : MonoBehaviour {

	public string texto;
	public float maximoTempoVisivel = 0;
	private string nomeSkin = "IndicadorLocalizacao";
	
	public float textoOffSetLateral = 15.0F;
	public float textoOffSetSuperior = 5.0F;
	
	private int alturaTexto = 40;

	public bool exibir = false;
	private float tempoVisivel = 0;
	
	

	void OnTriggerEnter(Collider outro)
	{
		exibir = (outro.gameObject.tag == "Player");		
		tempoVisivel = 0;
	}
	
	void OnTriggerExit(Collider outro)
	{
		exibir = false;
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
			
			Rect posicaoTexto = new Rect (textoOffSetLateral, textoOffSetSuperior, Screen.width, alturaTexto);
			
			GUI.Label (posicaoTexto, texto, nomeSkin);		
		}
	}
	
}
