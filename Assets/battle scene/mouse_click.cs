using UnityEngine;
using System.Collections;

public class mouse_click : MonoBehaviour {

	private Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//rend.material.shader = Shader.Find ("Specular");
		//rend.material.SetColor ("_SpecColor", Color.red);
		transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
