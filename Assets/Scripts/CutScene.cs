using UnityEngine;
using System.Collections;

public class CutScene : MonoBehaviour
{
    GameObject leftParty;
    GameObject rightParty;
    GameObject background;

    SpriteRenderer lRend;
    SpriteRenderer rRend;
    SpriteRenderer bRend;

    bool rendererSet = false;

    int leftOffsetX = 2;
    int rightOffsetX = -2;
    int mobOffSetZ = 4;
    int mobOffSetY = 1;

    int backgroundOffset = 5;

    int camYOffset = 0;

    float fadeTime = 0.3f;
    //float fadeSpeed = 0.2f;
    float min = 0.6f;
    float max = 1.0f;

    bool turn = false; //false : left, true : right
    bool fading = false;
    
    float currentTime = 0.0f;

    // Use this for initialization
    void Awake(){

        leftParty = new GameObject();
        rightParty = new GameObject();
        background = new GameObject();
    }

    void Update()
    {
        if (fading)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= fadeTime)
            {
                if (turn)
                {
                    float t = currentTime / fadeTime;
                    lRend.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(min, max,t));
                    rRend.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(max, min, t));
                }
                else
                {
                    float t = currentTime / fadeTime;
                    lRend.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(max, min,t));
                    rRend.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(min, max, t));
                }


                /*if (turn)
                {
                    FadeIn(lRend);
                    FadeOut(rRend);
                    turn = false;
                }
                else
                {
                    FadeIn(rRend);
                    FadeOut(lRend);
                    turn = true;
                }*/
            }
            else
            {
                turn = !turn;
                fading = false;
                currentTime = 0.0f;
            }
        }

    }

    void FadeIn(SpriteRenderer rend)
    {
        //rend.material.color.a = Mathf.Lerp(rend.material.color.a, 0, Time.deltaTime * fadeSpeed);

    }

    void FadeOut(SpriteRenderer rend)
    {

    }

    public void ChangeSpeaker()
    {
        fading = true;
    }

    public void SetSprites(Sprite p, Sprite e, Sprite b, Vector3 mainCameraPos, bool turn)
    {
        this.turn = turn;

        if (!rendererSet)
        {
            lRend = leftParty.AddComponent<SpriteRenderer>();
            rRend = rightParty.AddComponent<SpriteRenderer>();
            bRend = background.AddComponent<SpriteRenderer>();
            rendererSet = true;
        }


        Vector3 lV = new Vector3(mainCameraPos.x - leftOffsetX,  mainCameraPos.y - mobOffSetY, mainCameraPos.z + mobOffSetZ);
        Vector3 rV = new Vector3(mainCameraPos.x - rightOffsetX, mainCameraPos.y - mobOffSetY, mainCameraPos.z + mobOffSetZ);
        Vector3 bV = new Vector3(mainCameraPos.x,                mainCameraPos.y + camYOffset, mainCameraPos.z + backgroundOffset);

        lRend.sprite = p;
        rRend.sprite = e;
        bRend.sprite = b;

        if (turn)
        {
            lRend.color = new Color(1f, 1f, 1f, 0.2f);
        }
        else
        {
            rRend.color = new Color(1f, 1f, 1f, 0.2f);
        }

        leftParty.transform.position = lV;
        rightParty.transform.position = rV;
        background.transform.position = bV;
    }
}
