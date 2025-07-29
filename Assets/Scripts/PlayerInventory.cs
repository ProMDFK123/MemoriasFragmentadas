using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> collectedItems = new List<ItemData>();
    public GameObject inventoryUI;
    public Transform container;
    public GameObject pagePrefab;

    private List<GameObject> pages = new List<GameObject>();
    private int currentPageIndex = 0;

    public void AddItem(ItemData item)
    {
        if (!collectedItems.Contains(item))
        {
            collectedItems.Add(item);
            Debug.Log("item añadido al inventario");
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

    void UpdateInventoryUI()
    {
        foreach (Transform child in container) Destroy(child.gameObject);
        pages.Clear();

        int itemsPerPage = 4;
        int totalPages = Mathf.CeilToInt((float)collectedItems.Count / itemsPerPage);

        for (int p = 0; p < totalPages; p++)
        {
            GameObject page = Instantiate(pagePrefab, container);
            page.SetActive(p == 0);
            pages.Add(page);

            Transform slotPar = page.transform;
            for (int i = 0; i < itemsPerPage; i++)
            {
                int idx = p * itemsPerPage + i;
                if (idx >= collectedItems.Count) break;

                var item = collectedItems[idx];
                var slot = slotPar.GetChild(i);
                var slotUI = slot.GetComponent<ItemSlotUI>();
                if (slotUI != null) slotUI.Set(item);
            }
        }

        currentPageIndex = 0;
    }

    public void NextPage()
    {
        if (currentPageIndex + 1 >= pages.Count) return;
        pages[currentPageIndex].SetActive(false);
        currentPageIndex++;
        pages[currentPageIndex].SetActive(true);
    }

    public void PreviousPage()
    {
        if (currentPageIndex <= 0) return;
        pages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);
    }

    public bool HasItem(string id)
    {
        return collectedItems.Exists(i => i.itemID == id);
    }
}
