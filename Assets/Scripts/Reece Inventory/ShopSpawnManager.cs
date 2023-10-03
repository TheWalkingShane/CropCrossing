using UnityEngine;

public class ShopSpawnManager : MonoBehaviour
{
    public GameObject[] inventorySlots; // Assign the inventory slot transforms in the Inspector
    public GameObject tester;
    
    private int currentSlotIndex = 0; // To keep track of the current slot

    private void Start()
    {
        // Initialize the current slot index
        currentSlotIndex = 0;
        
        //Test Code:
        SpawnItemInSlot(tester);
    }

    public bool SpawnItemInSlot(GameObject item)
    {
        // Check if all slots are filled
        if (currentSlotIndex >= inventorySlots.Length)
        {
            Debug.LogError("Error: Slots filled");
            return false;
        }

        // Iterate through the inventory slots
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            GameObject currentSlot = inventorySlots[i];

            // Check if the current slot has no children
            if (currentSlot.transform.childCount == 0)
            {
                // Instantiate the item without inheriting scale
                GameObject spawnedItem = Instantiate(item, currentSlot.transform.position, Quaternion.identity);
            
                // Make the spawned item a child of the current slot
                spawnedItem.transform.parent = currentSlot.transform;
                
                // Increment the current slot index
                currentSlotIndex++;

                return true; // Return true to indicate that the item was successfully spawned
            }
        }

        // If all slots are already filled, return false
        Debug.LogError("Error: Slots filled");
        return false;
    }
}