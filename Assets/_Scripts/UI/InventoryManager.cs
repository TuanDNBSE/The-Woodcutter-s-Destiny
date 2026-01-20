using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // List of items in the inventory
    public List<string> items = new List<string>();

    // Add item to inventory
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Picked up: " + itemName);
        // TODO: Update UI to show item icon
    }

    // Use item
    public void UseItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            Debug.Log("Using: " + itemName);
            if (itemName == "NieuComThan")
            {
                // Call PlayerHealth script to heal
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(50); // heal 50 HP
                }
            }
            items.Remove(itemName);
        }
        else
        {
            Debug.Log("Item not found: " + itemName);
        }
    }
}
