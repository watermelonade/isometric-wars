using UnityEngine;
using System.Collections;


public class Powerup : InventoryItem
{
    float damageIncrease { get; set; }
    public Powerup(string pname, float pweight, float pdamageIncrease, Sprite psprite)
    {
        itemName = pname;
        weight = pweight;
        sprite = psprite;
        damageIncrease = pdamageIncrease;
    }
}