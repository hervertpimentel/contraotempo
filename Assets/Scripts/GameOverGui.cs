using UnityEngine;
using System.Collections;

/**
 * Controla a tela de game Over do jogo
 * criando botoes de opcoes iniciar uma nova partida o sair do jogo
 * bem como exibir a tela de 
 */


[ExecuteInEditMode()] 
public class GameOverGui : MonoBehaviour {

	public Texture2D background;
	private bool carregando = false;
	public Texture2D botaoJogar;
	public Texture2D botaoSair;
	
	
	void Update () 
	{
		if ( Input.GetButtonUp("Acao") )
		{
			executarJogar();
		}	
		
		if ( Input.GetButtonUp("Cancelar") )
		{
			executarSair();
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
		
		if (GUI.Button(new Rect(20, Screen.height - 150, 140, 140), botaoJogar, new GUIStyle())) 
		{
			executarJogar();
		}	
		
		if (GUI.Button (new Rect (180, Screen.height - 150, 140, 140), botaoSair, new GUIStyle())) {
			executarSair();
		}
		
		
		if (carregando){
			GUI.Label (new Rect( 20, 60, 400, 70), "Carregando...");		
		}
	}
	
	
	
	private void executarJogar() {
		carregando = true;
		Application.LoadLevel("Cenario");
		
	}
	
	private void executarSair() {
		Application.Quit ();		
	}
}
