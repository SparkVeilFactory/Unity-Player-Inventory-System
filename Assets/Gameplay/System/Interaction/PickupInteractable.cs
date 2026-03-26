using UnityEngine;

public class PickupInteractable : InteractableBase
{
    public string itemName = "Item";

    public override void Interact()
    {
        if (InventoryManager.Instance != null)
        {
            bool added = InventoryManager.Instance.AddItem(itemName);

            if (added)
            {
                Debug.Log("Picked up " + itemName);
                gameObject.SetActive(false);
            }
        }
    }
}