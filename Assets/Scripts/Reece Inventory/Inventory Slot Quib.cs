using UnityEngine;

public class InventorySlotQuib : MonoBehaviour
{
    public bool isOccupied = false; // Indicates whether the slot is occupied
    
    private void Start()
    {
        // Check for collisions with items when the scene starts
        CheckForCollisionsWithCrops();
    }

    private void CheckForCollisionsWithCrops()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // Adjust the radius as needed

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Drag"))
            {
                // Handle the case as if the item is already slotted into this slot
                isOccupied = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drag") && !isOccupied)
        {
            // Snap the item to the slot
            other.transform.position = transform.position;
            isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drag"))
        {
            // Item left the slot area
            isOccupied = false;
        }
    }
}