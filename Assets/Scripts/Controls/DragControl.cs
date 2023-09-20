using UnityEngine;

public class DragControl : MonoBehaviour
{
    public bool isDragging = false;          // Track if an object is currently being dragged
    public Transform potentialLockTarget;    // Potential "Lock" object the "Drag" object might snap to
    public Transform previousLockTarget;     // The last "Lock" object the "Drag" object was snapped to

    private Vector3 offset;                  // Offset between mouse position and the object's position
    private Transform objectBeingDragged;    // Reference to the object currently being dragged

    void Update()
    {
        // If left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Drag"))
            {
                isDragging = true;
                objectBeingDragged = hit.transform;
                offset = objectBeingDragged.position - hit.point;
            }
        }

        // If left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Snap to new "Lock" object logic
            if (objectBeingDragged && potentialLockTarget)
            {
                objectBeingDragged.position = potentialLockTarget.position;
                previousLockTarget = potentialLockTarget;
            }
            // If not locked to a new position, return to previous "Lock"
            else if (objectBeingDragged && previousLockTarget)
            {
                objectBeingDragged.position = previousLockTarget.position;
            }

            objectBeingDragged = null;
            potentialLockTarget = null;
        }

        // While dragging the object
        if (isDragging && objectBeingDragged)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                objectBeingDragged.position = hit.point + offset;
            }
        }

        // If not dragging and no potential lock, return to the previous lock (if available)
        if (!isDragging && !potentialLockTarget && objectBeingDragged && previousLockTarget)
        {
            objectBeingDragged.position = previousLockTarget.position;
        }
    }
}


