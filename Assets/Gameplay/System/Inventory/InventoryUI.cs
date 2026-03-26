using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public List<InventorySlotUI> slots = new List<InventorySlotUI>();

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshUI()
    {
        List<InventoryItem> items = InventoryManager.Instance.GetItems();

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                string displayText = items[i].itemName;

                if (items[i].quantity > 1)
                {
                    displayText += " x" + items[i].quantity;
                }

                slots[i].SetItem(displayText);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}