using UnityEngine;
using System.Collections;

public class Porta : MonoBehaviour {
	
	public GameObject mesh;
	
	
	public bool aberta;
	public float fecharEm = 20;

	private Animation animador;	
	
	private float tempoAberta = 0;
	public string animacao = "acao";


	private string animacaoObjeto;
	private AudioSource emisorDeSom;

	void Start()
	{
		animador = mesh.animation;
		animacaoObjeto = GameAssistente.pegaNomeDaAnimacao(0, mesh);

		emisorDeSom = gameObject.AddComponent<AudioSource>();
		emisorDeSom.volume = 0.6f;
	}
	
	void Update () 
	{
		if (aberta) 
			tempoAberta += Time.deltaTime;
		
		if (tempoAberta >= fecharEm)
			fechar();
	}
	
	
	void acao()
	{
		//faz a acao do player...
		GameObject player = GameAssistente.instance.player;
		player.animation.Play(animacao);		

		if (!aberta){			
			abrir();
		} else {
			fechar();
		}
	}	
		
	void abrir()
	{
		emisorDeSom.PlayOneShot (GameAssistente.instance.somPortaAbrindo);

		animador[animacaoObjeto].speed = 1;
		animador[animacaoObjeto].wrapMode = WrapMode.Once;
		animador.Play(animacaoObjeto);
		
		aberta = true;
		tempoAberta = 0;
	}
	
	void fechar()
	{
		animador[animacaoObjeto].time = animador[animacaoObjeto].speed;
		animador[animacaoObjeto].speed = -1;
		animador[animacaoObjeto].wrapMode = WrapMode.Once;
		animador.Play(animacaoObjeto);

		GameAssistente.instance.tocarSomComDelay(emisorDeSom, GameAssistente.instance.somPortaFechando, 0.9f);

		aberta = false;		
		tempoAberta = 0;
	}
}
