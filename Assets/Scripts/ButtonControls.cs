using UnityEngine;
using System.Collections;



//Loads the New Game when the New Game button is pressed.



public class ButtonControls : MonoBehaviour {
    public bool isStartNG; //Start New Game
    public bool isStartLG; //Load Game
    public bool isStartExtras; //Extras
    public bool isStartOptions; //Options
    public bool isQuit; //Quit

    // Use this for initialization
    void Start () {
	    
	}
	
    void OnGUI()
    {
        

    }

    void OnMouseUp()
    {
        if (isStartNG)
        {
            Application.LoadLevel(1); //Loads the Scene for New Game
        }
        if (isStartLG)
        {
            Application.LoadLevel(2); //Loads Load Game Menu
        }
        if (isStartExtras)
        {
            Application.LoadLevel(3); //Loads Extras Menu
        }
        if (isStartOptions)
        {
            Application.LoadLevel(4); //Loads Options
        }
        if(isQuit)
        {
            Application.Quit(); //Quits the game
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
