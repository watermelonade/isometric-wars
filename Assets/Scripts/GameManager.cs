using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static bool inBattle = false;
    public static bool inCutScene = false;
    public static bool Paused = false;
    
    LevelManager LM;

    CutSceneManager CSM;

    // Use this for initialization
    void Start () {
        CSM = new CutSceneManager();
        LM = new LevelManager();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!inBattle && !inCutScene){

        }
	}

}
