using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public static bool IsOpen = false;

    public GameObject inventoryPanel;

    void Start()
    {
        inventoryPanel.SetActive(false);
        IsOpen = false;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB pressed");
            ToggleInventory();
        }

        if (IsOpen && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed inside inventory");

            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.DropHoveredItem();
            }
            else
            {
                Debug.Log("InventoryManager.Instance is NULL");
            }
        }
    }

    void ToggleInventory()
    {
        IsOpen = !IsOpen;
        inventoryPanel.SetActive(IsOpen);

        if (IsOpen)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}