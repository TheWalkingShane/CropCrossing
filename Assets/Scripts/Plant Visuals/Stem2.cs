using UnityEngine;

public class Stem2 : MonoBehaviour
{
    #region Stem Settings

    [Header("Stem Settings")] 
    public Color stemColor;
    public Vector3 size;
    public Vector3 rotation;

    [Tooltip("Images")]
    public Sprite coreImage;
    public Sprite stemImage;//Image of the stem itself

    [Tooltip("The connection point for the core.")]
    public GameObject connectionPoint;

    #endregion// The connection point for the core

    private void Start()
    {
        // Create the core object and set its attributes
        CreateCoreObject();
    }
    public void SetStemAttributes(Sprite image, Color color, Vector3 newSize)
    {
        SpriteRenderer stemRenderer = GetComponent<SpriteRenderer>();
        stemRenderer.sprite = image;
        stemColor = color;
        size = newSize; // Set the size attribute
        UpdateStemVisuals(); // Call a method to update the stem visuals based on these attributes
    }

    private void UpdateStemVisuals()
    {
        SpriteRenderer stemRenderer = GetComponent<SpriteRenderer>();
        stemRenderer.color = stemColor;
        transform.localScale = size;    
    }

    private void CreateCoreObject()
    {
        // Instantiate the connection point and set its position
        GameObject newConnectionPoint = Instantiate(connectionPoint, transform.position, Quaternion.identity);
        newConnectionPoint.transform.SetParent(transform);

        // Set the core's position, rotation, and scale relative to the connection point
        newConnectionPoint.transform.localRotation = Quaternion.Euler(rotation); // Example rotation
        newConnectionPoint.transform.localScale += size;

        // Find the core object within the connection point (assuming it's a child)
        Transform coreTransform = newConnectionPoint.transform.Find("Core");

        if (coreTransform != null)
        {
            // Get the SpriteRenderer component of the core
            SpriteRenderer coreRenderer = coreTransform.GetComponent<SpriteRenderer>();

                // Check if a valid stemColor is provided before setting the core's color
                if (stemColor.a != 0f)
                {
                    coreRenderer.color = stemColor;
                } 
        }
    }
}