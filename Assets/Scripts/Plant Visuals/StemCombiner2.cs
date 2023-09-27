using UnityEngine;

public class StemCombiner2 : MonoBehaviour
{
    public GameObject objectToCopy11;
    public GameObject objectToCopy22;
    private GameObject objectToCopy1;
    private GameObject objectToCopy2;
    private GameObject cropCombination;
    private void Start()
    {
        objectToCopy1 = objectToCopy11;
        objectToCopy2 = objectToCopy22;
        //This start function is for testing purposes only
        if (objectToCopy1 != null || objectToCopy2 != null)
        {
            Debug.Log("Calling Combine Objects");
            CombineObjects();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crop"))
        {
            if (objectToCopy1 == null)
            {
                objectToCopy1 = collision.gameObject;
                Debug.Log("Set objectToCopy1 to " + objectToCopy1.name);
            }
            else if (objectToCopy2 == null)
            {
                objectToCopy2 = collision.gameObject;
                Debug.Log("Set objectToCopy2 to " + objectToCopy2.name);
            }
        }
        if (objectToCopy1 != null && objectToCopy2 != null)
        {
            CombineObjects();   
        }
        
    }
    private void CombineObjects()
    {
        // This is where you put the logic for combining objects
        Debug.Log("Combining objects: " + objectToCopy1.name + " and " + objectToCopy2.name);

        // Reset objectToCopy1 and objectToCopy2 to null if needed
        
        //Object 1 is our base crop.
        cropCombination = objectToCopy1;
        Stem2 cropCombinationStem = cropCombination.GetComponent<Stem2>();

        //Object 2 is where we shall inherit core image and color.
        Stem2 stem2Component = objectToCopy2.GetComponent<Stem2>();
        //Inheriting values here{
            cropCombinationStem.connectionPoint = stem2Component.connectionPoint;
            cropCombinationStem.size = stem2Component.size;
            cropCombinationStem.stemColor = stem2Component.stemColor;
        //{
        //Spawn object
        Instantiate(cropCombination, transform.position, Quaternion.identity);
        // Resetting objectToCopy1 and objectToCopy2 to null
        objectToCopy1 = null;
        objectToCopy2 = null;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crop"))
        {
            if (collision.gameObject == objectToCopy1)
            {
                objectToCopy1 = null;
                Debug.Log("Cleared objectToCopy1");
            }
            else if (collision.gameObject == objectToCopy2)
            {
                objectToCopy2 = null;
                Debug.Log("Cleared objectToCopy2");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding with: " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("No longer colliding with: " + collision.gameObject.name);
    }
}
