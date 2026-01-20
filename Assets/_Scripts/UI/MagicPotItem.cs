using UnityEngine;

public class MagicPotItem : MonoBehaviour
{
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inventoryManager != null)
            {
                inventoryManager.AddItem("NieuComThan");
                Debug.Log("Player picked up Magic Pot!");
            }

            // Remove item from scene after pickup
            Destroy(gameObject);
        }
    }
}
