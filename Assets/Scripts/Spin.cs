using UnityEngine;
using System.Collections;

public class Sping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ponto = GameAssistente.instance.player.transform.position;

		transform.LookAt (ponto);	
	}
}
