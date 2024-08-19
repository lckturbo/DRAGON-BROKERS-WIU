using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public List<ItemSlotData> items = new List<ItemSlotData>();

    // You can add other relevant data fields here, such as gold, player stats, etc.
}

[System.Serializable]
public class ItemSlotData
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public string itemDescription;
    public int worth;
    public float weight;
}
