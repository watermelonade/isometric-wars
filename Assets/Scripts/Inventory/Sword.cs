using UnityEngine;
using System.Collections;

public class Sword : InventoryItem
{
    public float damage { get; set; }

    public Sword(string pname, float pweight, float pdamage, Sprite psprite)
    {
        itemName = pname;
        weight = pweight;
        sprite = psprite;
        damage = pdamage;
    }
}
