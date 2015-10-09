using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 origin;
    private Vector3 difference;

    private bool drag = false;

	// Use this for initialization
	void Start () {
        startPos = CameraManager.battlePos;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetMouseButton(0))
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

            if(drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
        } else
        {
            drag = false;
        }

        if(drag == true)
        {
            Camera.main.transform.position = origin - difference;
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, startPos.y, Camera.main.transform.position.z);
        }

        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = startPos;
        }
	}
}
