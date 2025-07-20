using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Item added: " + itemName);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
