using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    float health;
    float recentHealth;
    float maxHealth;

    //in pixels
    float offsetY = 50f;
    float offsetX = 30f;

    Vector3 unitPos;
    Vector2 size = new Vector2(30,8);

    Vector3 screenPos;

    Texture2D empty;
    public Texture2D full;

    Rect barRect;
    Rect backRect;
    public Vector2 rectSize = new Vector2();


	// Use this for initialization
	void Start () {
        health = gameObject.GetComponent<FootUnit>().GetHP();
        recentHealth = health;

        maxHealth = gameObject.GetComponent<FootUnit>().GetMaxHP();

        unitPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
       
        screenPos = Camera.main.WorldToScreenPoint(unitPos);

        

        empty = new Texture2D((int)size.x,(int)size.y);
        full = new Texture2D((int)size.x, (int)size.y);

        //empty.SetPixel(1, 1, Color.red);
        //full.SetPixel(1, 1, Color.green);
        SetTexture(full, Color.green);
        SetTexture(empty, Color.red);

        
        //full.SetPixels(Color.green);

    }
	
	void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        barRect = new Rect(screenPos.x - offsetX, Screen.height - screenPos.y - offsetY, size.x * HealthPercent(), size.y);
        backRect = new Rect(screenPos.x - offsetX, Screen.height - screenPos.y - offsetY, size.x, size.y);
        //GUI.BeginGroup(new Rect(screenPos.x - offsetX, Screen.height-screenPos.y - offsetY, size.x, size.y));
        //GUI.contentColor = Color.green;
            GUI.DrawTexture(backRect, empty);

        //GUI.BeginGroup(new Rect(0, 0, size.x * health, size.y));
        //GUI.color = Color.green;
        //full.Resize((int)(size.x*HealthPercent()),(int)size.y);
        //GUI.color = Color.green;

            GUI.DrawTexture(barRect, full);
        
        //rectSize.x = size.x * HealthPercent();
        //rectSize.y = size.y;

    }

    void Update()
    {
        unitPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        screenPos = Camera.main.WorldToScreenPoint(unitPos);
       
        health = gameObject.GetComponent<FootUnit>().GetHP();

        if(health != recentHealth)
        {
            recentHealth = health;
            //ResizeBar();
        }

        if (health < 0)
        {
            //Destroy(gameObject);
        }
    }

    public void ResizeBar()
    {
        full.Resize((int)(size.x * HealthPercent()), (int)size.y);
        full.Apply(false);
        SetTexture(full, Color.green);
    }

    float HealthPercent()
    {
        return health / maxHealth;
    }

    /*public void AdjustHealth(float adjustment)
    {
        health += adjustment;
    }*/

    void SetTexture(Texture2D tex, Color color)
    {
        // colors used to tint the first 3 mip levels
        Color[] colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        int mipCount = Mathf.Min(3, tex.mipmapCount);

        // tint each mip level
        for (var mip = 0; mip < mipCount; ++mip)
        {
            var cols = tex.GetPixels(mip);
            for (var i = 0; i < cols.Length; ++i)
            {
                cols[i] = Color.Lerp(cols[i], color, 0.33f);
            }

            tex.SetPixels(cols, mip);
        }
        // actually apply all SetPixels, don't recalculate mip levels
        tex.Apply(false);
    }
}
