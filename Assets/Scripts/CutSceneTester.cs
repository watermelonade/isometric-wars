using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class CutSceneTester : MonoBehaviour
{
    GameObject cam;
    CutScene cut;
    DialogueBox box;

    StreamReader r;
    List<string> lines;
    int linenum = 0;

    string prevspeaker = "";

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        Sprite left = Resources.Load<Sprite>("Sprites/AndyAdvanceWars");
        Sprite right = Resources.Load<Sprite>("Sprites/AndyAdvanceWars-2");
        Sprite background = Resources.Load<Sprite>("Sprites/tmpbackground");

       
        cut = cam.AddComponent<CutScene>();
        box = cam.AddComponent<DialogueBox>();


        cut.SetSprites( left, right, background, cam.transform.position,false);

        lines = new List<string>();
        LoadLines("Assets/Resources/Dialogue/testdialogue.txt");
        box.SetText(lines[linenum]);
    }

    void LoadLines(string file)
    {
        r = File.OpenText(file);

        using (r)
        {
            string line = r.ReadLine();
            while (line != null)
            {
                lines.Add(line);
                line = r.ReadLine();
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space"))
        {

            if (linenum < lines.Count)
            {

                string s = lines[++linenum];
                string[] tmp = Regex.Split(s, @": ");


                if (tmp[0] != prevspeaker)
                {
                    cut.ChangeSpeaker();
                    box.SetText(tmp[1]);
                }
                else
                {
                    box.SetText(tmp[1]);
                }

                prevspeaker = tmp[0];
                //box.SetText(lines[linenum++]);
            }
        }
        
        
    }


}
