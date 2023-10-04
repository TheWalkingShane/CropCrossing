using System;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class StemCombiner2 : MonoBehaviour
{
    public GameObject objectToCopy1;
    public GameObject objectToCopy2;
    public GameObject printObject;
    public GameObject cropSlot;
    
    //these variables are needed for the UI to work.
    public GameObject statManager;
    public GameObject geneManger;
    public void CheckChildrenForStems()
    {
        Debug.Log("Calling checkchildren");
        // Get all the children of this Combiner object
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        // Count the number of children with 'Crop' tag and Stem2 components
        int cropCount = 0;

        foreach (Transform child in children)
        {
            if (child.CompareTag("Crop"))
            {
                Debug.Log("Crop Found");

                Stem2 stem2Component = child.GetComponent<Stem2>();
                if (stem2Component != null)
                {
                    Debug.Log("Looping Null");
                    cropCount++;

                    // Set objectToCopy1 and objectToCopy2 to the found Crop objects
                    if (cropCount == 1)
                    {
                        objectToCopy1 = child.gameObject;
                    }
                    else if (cropCount == 2)
                    {
                        objectToCopy2 = child.gameObject;
                    }

                    // If you have found two distinct Crop objects, combine them
                    if (cropCount == 2)
                    {
                        Debug.Log("Calling Combine Objects");
                        CombineObjects();
                        break; // You can exit the loop once you've found two Crop objects
                    }
                }
            }
        }
    }
    
    private void CombineObjects()
    {
        if (objectToCopy1 != null && objectToCopy2 != null)
        {
            Debug.Log("Combing " + objectToCopy1 + " and " + objectToCopy2);
            // This is where you put the logic for combining objects
            Debug.Log("Combining objects: " + objectToCopy1.name + " and " + objectToCopy2.name);

            // Create a new cropSlot in the same space as objectToCopy1
            GameObject newCropSlot = Instantiate(cropSlot, printObject.transform.position, Quaternion.identity);

            // Create a new copy of objectToCopy1 as a child of printObject
            GameObject newCropCombination = Instantiate(objectToCopy1, printObject.transform);
            //newCropCombination.transform.localScale = (Vector3.one/5);
            Stem2 cropCombinationStem = newCropCombination.GetComponent<Stem2>();

            // Object 2 is where we shall inherit core image and color.
            Stem2 stem2Component = objectToCopy2.GetComponent<Stem2>();

            // Calculate the average color between objectToCopy1 and objectToCopy2
            Color averageColor = (cropCombinationStem.stemColor + stem2Component.stemColor) / 2.0f;

            // Inheriting values here
            cropCombinationStem.coreImage = stem2Component.coreImage;
            cropCombinationStem.connectionPoint = stem2Component.connectionPoint;
            cropCombinationStem.size = (cropCombinationStem.size + stem2Component.size);
            cropCombinationStem.stemColor = averageColor;
            Debug.Log("New size: " + cropCombinationStem.size);

            // Make newCropCombination a child of newCropSlot
            newCropCombination.transform.parent = newCropSlot.transform;
            
            newCropCombination.transform.localScale = Vector3.one/27f;
            
            Core2[] core2Components = newCropCombination.GetComponentsInChildren<Core2>();
            foreach (Core2 core2 in core2Components)
            {
                Destroy(core2.gameObject);
            }
            
            this.GetComponent<Hybridization>().BeginHybridProcess(objectToCopy1, objectToCopy2, newCropCombination);
            newCropCombination.transform.parent.GetComponent<DisplayStatsScript>().setTextHolderObjects(geneManger, statManager);
            // Resetting objectToCopy1 and objectToCopy2 to null
            objectToCopy1 = null;
            objectToCopy2 = null;
            // Nudge newCropCombination's position down by -0.5 units along the X and Y axes
            //newCropCombination.transform.localPosition += new Vector3(-0.4013103f, -0.3f, 0f);
        }
        else
        {
            throw new System.Exception("Either objectToCopy1 and objectToCopy2 are null.");
        }
    }
}
