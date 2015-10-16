using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {


    List<InventoryItem> items;

	// Use this for initialization
	void Start () {
        items = new List<InventoryItem>();
	}
	
	
    public void AddItem(InventoryItem.ItemType item)
    {
        items.Add(InventoryItem.CreateItem(item));
    }

    public void DeleteItem(InventoryItem.ItemType itemType)
    {
        foreach(InventoryItem it in items)
        {
            if (it.type == itemType)
                items.Remove(it);
        }
    }



}
