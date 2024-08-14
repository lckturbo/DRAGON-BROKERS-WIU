using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }
}
