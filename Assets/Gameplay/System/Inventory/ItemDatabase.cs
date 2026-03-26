using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab; // 🔥 NOVO
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public List<ItemData> items = new List<ItemData>();

    private Dictionary<string, ItemData> itemDict = new Dictionary<string, ItemData>();

    private void Awake()
    {
        Instance = this;

        foreach (var item in items)
        {
            itemDict[item.itemName] = item;
        }
    }

    public Sprite GetIcon(string itemName)
    {
        if (itemDict.ContainsKey(itemName))
        {
            return itemDict[itemName].icon;
        }

        return null;
    }

    // 🔥 NOVO
    public GameObject GetPrefab(string itemName)
    {
        if (itemDict.ContainsKey(itemName))
        {
            return itemDict[itemName].prefab;
        }

        return null;
    }
}