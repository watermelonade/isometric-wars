using UnityEngine;
using System.Collections;

public class ChoiceMenu : MonoBehaviour {

    Vector2 menuSize = new Vector2(70, 60);
    Vector2 buttonSize = new Vector2(70, 20);

    Vector3 unitPos;
    Vector3 screenPos;

    float offsetX = 25;
    float offsetY = 25;


    void OnGUI()
    {
        GUI.BeginGroup(new Rect(screenPos.x+offsetX, Screen.height - screenPos.y - offsetY,menuSize.x,menuSize.y));

            if(GUI.Button(new Rect(0, 0,buttonSize.x,buttonSize.y), "Attack"))
            {

            }

            if (GUI.Button(new Rect(0, buttonSize.y*1, buttonSize.x, buttonSize.y), "Items"))
            {

            }

            if (GUI.Button(new Rect(0, buttonSize.y*2, buttonSize.x, buttonSize.y), "Wait"))
            {
                gameObject.GetComponent<FootUnit>().Finish();
            }

        GUI.EndGroup();



    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        unitPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        screenPos = Camera.main.WorldToScreenPoint(unitPos);

    }


}
