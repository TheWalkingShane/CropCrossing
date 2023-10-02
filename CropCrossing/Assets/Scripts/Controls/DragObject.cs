using UnityEngine;

public class DragObject : MonoBehaviour
{
    public DragControl controlScript; // Reference to the DragControl script on the camera

    private void Start()
    {
        controlScript = Camera.main.GetComponent<DragControl>();
    }

    // Removing the logic that clears the previousLockTarget from OnMouseDown
    private void OnMouseDown() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lock") && controlScript.isDragging)
        {
            controlScript.potentialLockTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lock") && controlScript.isDragging)
        {
            controlScript.potentialLockTarget = null;
        }
    }
}

