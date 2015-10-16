using UnityEngine;
using System.Collections;

public class Shield : InventoryItem
{
    public float defense { get; set; }
    public Shield(string pname, float pweight, float pdefense, Sprite psprite)
    {
        itemName = pname;
        weight = pweight;
        sprite = psprite;
        defense = pdefense;

    }