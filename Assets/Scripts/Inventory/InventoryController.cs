using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour {

    InventoryState state;

    int numBoxes = 4;
    int boxWidth, boxHeight = 50;



    public enum InventoryState
    {
        ShowGUI,
        HideGUI
    }

	// Use this for initialization
	void Start () {
        state = InventoryState.HideGUI;
	}
	
	void OnGUI()
    {
        if(state == InventoryState.ShowGUI)
        {
            GUI.BeginGroup(new Rect(Screen.width - Screen.width / 4, 0, Screen.width / 4, Screen.height));
                    
                for(int i = 0; i < numBoxes; i++)
                {
                    
                }
                
                
            GUI.EndGroup();
        }
    }

}
