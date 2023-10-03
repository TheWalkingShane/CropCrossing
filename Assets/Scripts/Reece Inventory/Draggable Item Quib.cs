using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItemQuib : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPosition;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // Find the nearest slot and snap the item to it
        SnapToNearestSlot();
    }

    private void SnapToNearestSlot()
    {
        InventorySlotQuib[] slots = FindObjectsOfType<InventorySlotQuib>(); // Find all available slots

        if (slots.Length == 0)
        {
            Debug.LogWarning("No slots found in the scene.");
            return;
        }

        InventorySlotQuib nearestSlot = null;
        float shortestDistance = float.MaxValue;
        bool isAlreadyIntersecting = false;

        // Check if the item is already intersecting with a slot
        foreach (InventorySlotQuib slot in slots)
        {
            if (slot.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                isAlreadyIntersecting = true;
                nearestSlot = slot;
                break;
            }
        }

        // If it's not already intersecting, find the nearest slot
        if (!isAlreadyIntersecting)
        {
            foreach (InventorySlotQuib slot in slots)
            {
                if (!slot.isOccupied)
                {
                    float distance = Vector3.Distance(transform.position, slot.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSlot = slot;
                    }
                }
            }
        }

        if (nearestSlot != null)
        {
            if (!isAlreadyIntersecting)
            {
                // Snap the item to the nearest slot's position
                StartCoroutine(SmoothMoveToSlot(nearestSlot.transform.position));

                // Optionally, notify the slot that it's occupied and set references.
                nearestSlot.isOccupied = true;
            
                // Set the item as a child of the slot
                transform.SetParent(nearestSlot.transform);
            }
            else
            {
                // The item is already intersecting with a slot, just set its position
                transform.position = nearestSlot.transform.position;
            }
        }
    }

    private IEnumerator SmoothMoveToSlot(Vector3 targetPosition)
    {
        float duration = 0.2f; // Adjust the duration of the movement
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition; // Ensure it snaps exactly to the slot's position
    }
}