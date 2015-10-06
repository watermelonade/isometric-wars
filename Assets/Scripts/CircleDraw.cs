using UnityEngine;
using System.Collections;

public class CircleDraw : MonoBehaviour 
{
    bool drawing = false;

    public GameObject circle;
    Vector3 scale = new Vector3(2f, .01f, 2f);

    float dampRadius = 0f;
    float targetRadius;

    void Start()
    {
        
    }

    void Update()
    {
        if (drawing)
        {
            
            if (dampRadius < targetRadius)
            {
                float tmp = Mathf.SmoothDamp(circle.transform.localScale.y, targetRadius, ref dampRadius, .9f);
                scale.x = scale.z = dampRadius;
                scale.y = 0.1f;
                circle.transform.localScale = scale;
            } else
            {
                drawing = false;
            }
        }
    }

    public void MakeCircle(float radius, Vector3 pos)
    {
        circle = Instantiate(Resources.Load("Prefabs/PlayerRange 1", typeof(GameObject))) as GameObject;
        //circle.AddComponent<SphereCollider>().radius = radius;
        //circle.AddComponent<Renderer>();

        //scale.x = scale.z = radius;
        Color color = circle.GetComponent<Renderer>().material.color;
        color.a = 0.1f;

        circle.GetComponent<Renderer>().material.color = color;

        circle.transform.position = new Vector3(pos.x, 1f, pos.z);

        targetRadius = radius;

        drawing = true;

        //circle.transform.localScale = scale;

    }

    public void RemoveCircle()
    {
        drawing = false;
        Destroy(circle);
    }
}