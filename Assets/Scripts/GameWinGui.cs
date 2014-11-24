using UnityEngine;
using System.Collections;

/**
 * Controla a tela de boas vindas do jogo
 * disponibiliza opcoes para entrar no jogo
 * ou sair do programa
 *
 */

[ExecuteInEditMode()] 
public class GameWinGui : MonoBehaviour {

	public Texture2D background;
	private bool carregando = false;

	private float tempoNaTela = 0;

	void Update () 
	{
		if ( Input.GetButtonUp("Acao") )
		{
			executarMenu();
		}	
		
		if ( Input.GetButtonUp("Cancelar") )
		{
			executarMenu();
		}	

		tempoNaTela += Time.deltaTime;


		//depois de 15seg carrega novamente o menu do jogo
		if (tempoNaTela >= 15) {
			executarMenu();
		}
	}
	
	
	void OnGUI()
	{
		//vou precisar disso depois pros estilos de butoa e tudo mais...
		//GUI.skin = GameAssistente.instance.gameGuiSkin;
		
		GUIStyle backStyle = new GUIStyle();
		backStyle.normal.background = background;
		
		Rect retangulo = new Rect(0F, 0F, Screen.width, Screen.height);
		GUI.Label(retangulo, "", backStyle);
		
		if (carregando){
			GUI.Label (new Rect( 20, 60, 400, 70), "Carregando...");		
		}
	}

	
	private void executarMenu() {
		carregando = true;
		Application.LoadLevel("Menu");
		
	}
}
