using UnityEngine;
using System.Collections;

public class TexturaAnimada : MonoBehaviour {

	public MovieTexture Video;
	public bool RepetirVideo = true;

	// Use this for initialization

	void Start () {
		renderer.material.mainTexture = Video;
		Video.Play ();
		Video.loop = RepetirVideo;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
