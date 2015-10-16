using UnityEngine;
using System.Collections;

public class HealthItem : InventoryItem {

    public float healing { get; set; }

    public HealthItem(string pname, float pweight, float phealing, Sprite psprite)
    {
        type = ItemType.HealthItem;
        itemName = pname;
        weight = pweight;
        sprite = psprite;
        healing = phealing;
    }
}
