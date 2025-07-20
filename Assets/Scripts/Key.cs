using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string id = "ID";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(id);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Inventory component not found on player.");
            }
        }
    }
}
