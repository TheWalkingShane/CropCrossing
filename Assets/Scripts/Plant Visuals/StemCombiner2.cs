using UnityEngine;

public class StemCombiner2 : MonoBehaviour
{
    public GameObject objectToCopy11;
    public GameObject objectToCopy22;
    private GameObject objectToCopy1;
    private GameObject objectToCopy2;
    private GameObject cropCombination;
    
    private Collider2D myCollider;
    private bool hasCombined = false; // Our flag
    public InventorySlot[] inventorySlots; 

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
        objectToCopy1 = objectToCopy11;
        objectToCopy2 = objectToCopy22;

        if (objectToCopy1 != null || objectToCopy2 != null)
        {
            Debug.Log("Calling Combine Objects");
            CombineObjects();
        }
    }

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(myCollider.bounds.center, myCollider.bounds.size, 0f);
        foreach (var hitCollider in hitColliders)
        {
            OnTriggerEnter2D(hitCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Crop"))
        {
            
            if (objectToCopy1 == null)
            {
                objectToCopy1 = collider.gameObject;
                Debug.Log("Set objectToCopy1 to " + objectToCopy1.name);
            }
            else if (objectToCopy2 == null)
            {
                objectToCopy2 = collider.gameObject;
                Debug.Log("Set objectToCopy2 to " + objectToCopy2.name);
            }
        }

        if (objectToCopy1 != null && objectToCopy2 != null && !hasCombined)
        {
            CombineObjects();   
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Crop"))
        {
            //Debug.Log("bye");
            if (collider.gameObject == objectToCopy1)
            {
                objectToCopy1 = null;
                Debug.Log("Cleared objectToCopy1");
            }
            else if (collider.gameObject == objectToCopy2)
            {
                objectToCopy2 = null;
                Debug.Log("Cleared objectToCopy2");
            }
        }

        if (objectToCopy1 == null && objectToCopy2 == null)
        {
            hasCombined = false; // Reset the flag when both objects have exited
        }
    }

    private void CombineObjects()
    {
        Debug.Log("Combining objects: " + objectToCopy1.name + " and " + objectToCopy2.name);

        cropCombination = objectToCopy1;
        Stem2 cropCombinationStem = cropCombination.GetComponent<Stem2>();

        Stem2 stem2Component = objectToCopy2.GetComponent<Stem2>();
        
        cropCombinationStem.connectionPoint = stem2Component.connectionPoint;
        cropCombinationStem.size = stem2Component.size;
        cropCombinationStem.stemColor = stem2Component.stemColor;
        Debug.Log("Creating a clone at: " + Time.time);

        GameObject clonedCrop = Instantiate(cropCombination, transform.position, Quaternion.identity);
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)  // Check if the slot is empty
            {
                clonedCrop.transform.SetParent(slot.transform);  // Move the cloned crop into the slot
                clonedCrop.transform.localPosition = Vector3.zero;  // Optionally reset its local position
                break;  // Exit the loop as we've found a slot
            }
        }
        Debug.Log("Creating a clone at: " + Time.time);

        //Instantiate(cropCombination, transform.position, Quaternion.identity);
        objectToCopy1 = null;
        objectToCopy2 = null;

        hasCombined = true; // Set the flag once combination is done
    }
}
