using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	private Renderer rend;
    private Color startcolor;

	public bool enable = true;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
        rend.material.color = startcolor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        if(enable) 
			rend.material.color = Color.green;
    }

    void OnMouseExit()
    {
        if(enable) 
			rend.material.color = startcolor;
    }

	void OnMouseDown(){
        //rend.material.shader = Shader.Find ("Specular");
        //rend.material.SetColor ("_SpecColor", Color.red);
        //transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
        //rend.material.color = Color.red;
        //enable = false;
        if (enable)
            GameObject.Find("Main Camera").SendMessage("TileClicked", gameObject.transform.position);
	    
    }
}
