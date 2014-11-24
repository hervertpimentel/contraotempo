using UnityEngine;
using System.Collections;

public class FadeinOut : MonoBehaviour {

	public Texture2D fadeOutTexture ;
	public float fadeSpeed = 0.3f;
	
	private int drawDepth = -1000;	
	private float alpha = 1.0f; 
	private int fadeDir = -1;
	
	void Start () {
		alpha = 1;
		fadeIn();
	}
	
	
	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;	
		alpha = Mathf.Clamp01(alpha);	
		
		GUI.color = new Color() { a = alpha };
		
		GUI.depth = drawDepth;
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
	}
	
	void fadeIn(){
		fadeDir = -1;	
	}
	
	
	void fadeOut(){
		fadeDir = 1;	
	}
}
