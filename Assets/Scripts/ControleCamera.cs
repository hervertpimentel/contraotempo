using UnityEngine;
using System.Collections;

public class ControleCamera : MonoBehaviour {

	public Transform alvo;
	RaycastHit hit = new RaycastHit ();
	public float mouseX = 0;
	public float mouseY = 0;
	public float distanCam = 0;
	

	void Update () 
	{

		float horizontal = Input.GetAxis("Mouse X") * mouseX ;
		horizontal = Input.GetAxis ("Horizontal") * mouseX ;
		horizontal = horizontal * Time.deltaTime * 60f;

		transform.RotateAround (alvo.position, transform.up, horizontal);

		Vector3 rotacao = transform.eulerAngles;
		rotacao.z = 0;
		transform.eulerAngles = rotacao;

		transform.position = alvo.position - transform.forward * distanCam;

		alvo.parent.transform.Rotate(0, horizontal, 0);

		if (Physics.Linecast (alvo.position, transform.position, out hit))
		{
			transform.position = hit.point + transform.forward * 0.2f;
		}

	}
}
