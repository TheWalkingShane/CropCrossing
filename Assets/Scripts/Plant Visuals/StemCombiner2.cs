using UnityEngine;

public class StemCombiner2 : MonoBehaviour
{
    public Stem2 stemPrefab; // Prefab for the new stem
    public Core2 corePrefab; // Prefab for the new core

    public Stem2 currentStem1; // First plant to combine
    public Stem2 currentStem2; // Second plant to combine

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided objects are stems
        Stem2 stem = other.GetComponent<Stem2>();

        if (stem != null)
        {
            // If currentStem1 is not set, assign it to the first stem
            if (currentStem1 == null)
            {
                currentStem1 = stem;
            }
            // If currentStem1 is already set, assign currentStem2 and create a new plant
            else if (currentStem2 == null)
            {
                currentStem2 = stem;
                CreateNewPlant();
            }
        }
    }

    private void CreateNewPlant()
    {
        if (stemPrefab == null || corePrefab == null || currentStem1 == null || currentStem2 == null)
        {
            Debug.LogError("Missing prefabs or stems for combination.");
            return;
        }

        // Calculate the averaged color
        Color averagedColor = (currentStem1.stemColor + currentStem2.stemColor) / 2f;

        // Instantiate a new Stem and set its attributes
        Stem2 newStemObject = Instantiate(stemPrefab, transform.position, Quaternion.identity);
        Stem2 newStem = newStemObject.GetComponent<Stem2>();

        newStem.SetStemAttributes(currentStem1.stemImage, averagedColor, currentStem1.size);

        // Instantiate a new Core and set its attributes
        Core2 newCoreObject = Instantiate(corePrefab, newStem.transform.position, Quaternion.identity);
        Core2 newCore = newCoreObject.GetComponent<Core2>();

        // Assuming that the stemPrefab and corePrefab objects are set up correctly in the Inspector
        newCore.SetCoreAttributes(
            averagedColor,
            newStem.size, // Assuming that size is the correct property for core size
            newStem.rotation, // Assuming that rotation is the correct property for core rotation
            currentStem2.coreImage,
            newStem.connectionPoint // Assuming that connectionPoint is the correct property for core connection
        );

        // Reset the stored stems
        currentStem1 = null;
        currentStem2 = null;
    }
}
