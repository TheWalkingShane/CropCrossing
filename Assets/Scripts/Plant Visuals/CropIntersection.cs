using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropIntersection : MonoBehaviour
{
    public Stem stem1; // First plant we're crossbreeding
    public Stem stem2; // Second plant we're crossbreeding
    
    // Called when a crop object enters the trigger collider
    private void Update()
    {
        if (stem1 != null && stem2 != null)
        {
            CreateNewStemAndCore();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crop"))
        {
            Debug.Log("Crop entered trigger.");
            // Rest of your code...
        }

        if (other.CompareTag("Crop"))
        {
            // Get the Stem component of the intersected crop object
            Stem intersectedStem = other.GetComponentInParent<Stem>();

            // Check if we have two intersected stems
            if (stem1 == null)
            {
                stem1 = intersectedStem;
            }
            else if (stem2 == null && intersectedStem != stem1)
            {
                stem2 = intersectedStem;

                // Create a new stem and core
                CreateNewStemAndCore();
            }
        }
    }

    private void CreateNewStemAndCore()
    {
        // Calculate the averaged color
        Color averagedColor = (stem1.stemColor + stem2.stemColor) / 2f;

        // Instantiate a new stem
        GameObject newStemObject = Instantiate(stem1.gameObject, transform.position, Quaternion.identity);
        Stem newStem = newStemObject.GetComponent<Stem>();

        // Set the color of the new stem
        newStem.SetStemColor(averagedColor);

        // Optionally, you can adjust other properties of the new stem

        // Remove any existing cores from the new stem (assuming cores are children of the stem)
        foreach (Core core in newStem.cores)
        {
            Destroy(core.gameObject);
        }
    
        // Create a new core for the new stem
        Core newCore = Instantiate(stem1.cores[0], newStem.transform.position, Quaternion.identity);
    
        // Set core properties based on stem traits
        
        newCore.SetCoreAttributes(stem1.GetCoreImage(), averagedColor);

        // Add the new core to the new stem's list of cores
        newStem.cores.Add(newCore);

        // Reset the references for future intersections
        stem1 = null;
        stem2 = null;
        //The following is just being used while testing and we have nowhere to put crops
        StartCoroutine(DestroyStemAndCore(newStemObject, newCore));
    }
    
    private IEnumerator DestroyStemAndCore(GameObject stemObject, Core core)
    {
        yield return new WaitForSeconds(4f);

        Destroy(stemObject);
        Destroy(core.gameObject);
    }
}