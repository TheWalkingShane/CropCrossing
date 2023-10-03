using System;
using UnityEngine;

public class StemCombiner2 : MonoBehaviour
{
    public GameObject objectToCopy1;
    public GameObject objectToCopy2;
    private GameObject cropCombinationPrefab;
    
    private void Start()
    {
        // This start function is for testing purposes only
        if (objectToCopy1 != null || objectToCopy2 != null)
        {
            Debug.Log("Calling Combine Objects");
            CombineObjects();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject + "is colliding with " + other.gameObject);
        if (other.gameObject.CompareTag("Crop"))
        {
            if (objectToCopy1 == null)
            {
                objectToCopy1 = other.gameObject;
                Debug.Log("Set objectToCopy1 to " + objectToCopy1.name);
            }
            else if (objectToCopy2 == null)
            {
                objectToCopy2 = other.gameObject;
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

        // Create a new copy of objectToCopy1
        GameObject newCropCombination = Instantiate(objectToCopy1, transform.position, Quaternion.identity);
        Stem2 cropCombinationStem = newCropCombination.GetComponent<Stem2>();

        // Object 2 is where we shall inherit core image and color.
        Stem2 stem2Component = objectToCopy2.GetComponent<Stem2>();
        
        // Calculate the average color between objectToCopy1 and objectToCopy2
        Color averageColor = (cropCombinationStem.stemColor + stem2Component.stemColor) / 2.0f;

        // Inheriting values here
        cropCombinationStem.connectionPoint = stem2Component.connectionPoint;
        cropCombinationStem.size = (cropCombinationStem.size + stem2Component.size) / 2.0f;
        cropCombinationStem.stemColor = averageColor;

        // Resetting objectToCopy1 and objectToCopy2 to null
        objectToCopy1 = null;
        objectToCopy2 = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Crop"))
        {
            if (other.gameObject == objectToCopy1)
            {
                objectToCopy1 = null;
                Debug.Log("Cleared objectToCopy1");
            }
            else if (other.gameObject == objectToCopy2)
            {
                objectToCopy2 = null;
                Debug.Log("Cleared objectToCopy2");
            }
        }
    }

}