using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> collectedItems = new List<ItemData>();
    public GameObject inventoryUI;
    public Transform itemSlot;
    public GameObject itemSlotPrefab;

    public void AddItem(ItemData item)
    {
        if (!collectedItems.Contains(item))
        {
            collectedItems.Add(item);
            Debug.Log(item.itemName + "añadido al inventario");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory() {
        if (inventoryUI == null) return;

        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);

        if (!isActive) UpdateInventoryUI();
    }

    void UpdateInventoryUI() {
        foreach (Transform child in itemSlot) Destroy(child.gameObject);

        foreach (var item in collectedItems)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemSlot);
            var slotUI = slot.GetComponent<ItemSlotUI>();
            if (slotUI != null) slotUI.Set(item);
        }
    }

    public bool HasItem(string id) {
        return collectedItems.Exists(i => i.itemID == id);
    }
}
