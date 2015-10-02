using UnityEngine;
using System.Collections;

public class DialogueBox : MonoBehaviour
{

    bool newLine = false;

    string text = "";
    char[] textArray;

    int frameskip = 2;
    int currentframe = 0;
    int index = 0;
    int textsize = 0;

    GUIStyle style;
    
    void Start()
    {
        style = new GUIStyle();
    }

    void OnGUI()
    {
        style = GUI.skin.GetStyle("box");
        style.alignment = TextAnchor.UpperLeft;
        style.wordWrap = true;
        style.fontSize = 20;
        GUI.color = Color.black;
        GUI.backgroundColor = Color.blue;
        
        GUI.Label(new Rect(0, Screen.height-Screen.height/4, Screen.width, Screen.height/4), text, style);
    }

    // Update is called once per frame
    void Update()
    {
        if (newLine)
        {
            currentframe++;
            if (currentframe == frameskip)
            {
                currentframe = 0;
                if(index < textsize)
                {
                    text += textArray[index];
                    index++;
                }
            }

            
        }
    }

    public void SetText(string s)
    {
        text = "";
        index = 0;
        newLine = true;
        textsize = s.Length;
        textArray = s.ToCharArray();
    }

}
