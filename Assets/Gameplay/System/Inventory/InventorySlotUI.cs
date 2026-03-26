using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerMoveHandler,
    IPointerClickHandler
{
    public TextMeshProUGUI itemText;
    public Image itemIcon;

    private string currentItemName = "";
    private bool hasItem = false;

    public static InventorySlotUI hoveredSlot;

    public void SetItem(string itemName)
    {
        string baseName = GetBaseItemName(itemName);
        currentItemName = baseName;
        hasItem = !string.IsNullOrEmpty(baseName);

        int count = 1;

        if (itemName.Contains(" x"))
        {
            string[] parts = itemName.Split(" x");
            int.TryParse(parts[1], out count);
        }

        if (count > 1)
        {
            itemText.text = count.ToString();
        }
        else
        {
            itemText.text = "";
        }

        Sprite iconSprite = ItemDatabase.Instance.GetIcon(baseName);

        if (iconSprite != null)
        {
            itemIcon.sprite = iconSprite;
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.gameObject.SetActive(false);
            hasItem = false;
            currentItemName = "";
        }
    }

    public void ClearSlot()
    {
        currentItemName = "";
        hasItem = false;
        itemText.text = "";
        itemIcon.sprite = null;
        itemIcon.gameObject.SetActive(false);

        if (hoveredSlot == this)
        {
            hoveredSlot = null;
        }
    }

    private string GetBaseItemName(string displayName)
    {
        if (displayName.Contains(" x"))
        {
            return displayName.Split(" x")[0];
        }

        return displayName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredSlot = this;

        if (hasItem && TooltipUI.Instance != null)
        {
            TooltipUI.Instance.Show(currentItemName, eventData.position);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        hoveredSlot = this;

        if (hasItem && TooltipUI.Instance != null)
        {
            TooltipUI.Instance.UpdatePosition(eventData.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoveredSlot == this)
        {
            hoveredSlot = null;
        }

        if (TooltipUI.Instance != null)
        {
            TooltipUI.Instance.Hide();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasItem) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryManager.Instance.SplitItem(currentItemName);
        }
    }

    public bool HasItem()
    {
        return hasItem;
    }

    public string GetItemName()
    {
        return currentItemName;
    }
}