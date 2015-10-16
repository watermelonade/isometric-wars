using UnityEngine;
using System.Collections;

public abstract class InventoryItem {

    public string itemName { get; set; }
    public float weight { get; set; }
    public Sprite sprite { get; set; }
    public ItemType type { get; set; }

    public enum ItemType
    {
        HealthItem,
        Powerup,
        Sword,
        Shield
    }

    public static InventoryItem CreateItem(InventoryItem.ItemType ptype)
    {
        switch (ptype)
        {
            case ItemType.HealthItem:
                Sprite potion = Resources.Load<Sprite>("Sprites/shrek");
                return new HealthItem("Potion", 1, 3, potion);
                
            case ItemType.Powerup:
                Sprite powerup = Resources.Load<Sprite>("Sprites/shrek");
                return new Powerup("Powerup",1,.15f, powerup);

            case ItemType.Sword:
                Sprite sword = Resources.Load<Sprite>("Sprites/shrek");
                return new Sword("Sword", 5, 6, sword);

            case ItemType.Shield:
                Sprite shield = Resources.Load<Sprite>("Sprites/shrek");
                return new Shield("Shield", 6, 3, shield);

            default:
                return null;
        }

    }

}
