using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;

    public InventoryItem(string name, int amount)
    {
        itemName = name;
        quantity = amount;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int maxSlots = 9;

    [Header("Drop Settings")]
    public Transform player;
    public float dropDistance = 2f;

    [Header("Debug")]
    public List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                items[i].quantity++;

                if (InventoryUI.Instance != null)
                    InventoryUI.Instance.RefreshUI();

                return true;
            }
        }

        if (items.Count >= maxSlots)
            return false;

        items.Add(new InventoryItem(itemName, 1));

        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshUI();

        return true;
    }

    public bool SplitItem(string itemName)
    {
        if (items.Count >= maxSlots)
            return false;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName && items[i].quantity > 1)
            {
                items[i].quantity -= 1;
                items.Add(new InventoryItem(itemName, 1));

                if (InventoryUI.Instance != null)
                    InventoryUI.Instance.RefreshUI();

                return true;
            }
        }

        return false;
    }

    public void DropItem(string itemName)
    {
        Debug.Log("DropItem called for: " + itemName);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                GameObject prefab = ItemDatabase.Instance.GetPrefab(itemName);

                if (prefab == null)
                {
                    Debug.Log("Prefab is NULL for item: " + itemName);
                    return;
                }

                if (player == null)
                {
                    Debug.Log("Player reference is NULL");
                    return;
                }

                Vector3 forward = player.forward;
                forward.y = 0f;
                forward.Normalize();

                Vector3 spawnPos = player.position + forward * dropDistance;
                spawnPos.y = 1f;

                Debug.Log("Spawning at: " + spawnPos);

                GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(forward * 3f, ForceMode.Impulse);
                }

                Debug.Log("Prefab instantiated: " + prefab.name);

                items[i].quantity--;

                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i);
                }

                if (InventoryUI.Instance != null)
                    InventoryUI.Instance.RefreshUI();

                return;
            }
        }

        Debug.Log("Item not found in inventory list: " + itemName);
    }

    public void DropHoveredItem()
    {
        Debug.Log("Q pressed - trying to drop hovered item");

        if (InventorySlotUI.hoveredSlot == null)
        {
            Debug.Log("No hovered slot");
            return;
        }

        if (!InventorySlotUI.hoveredSlot.HasItem())
        {
            Debug.Log("Hovered slot has no item");
            return;
        }

        string itemName = InventorySlotUI.hoveredSlot.GetItemName();
        Debug.Log("Hovered item: " + itemName);

        DropItem(itemName);
    }

    public List<InventoryItem> GetItems()
    {
        return items;
    }
}