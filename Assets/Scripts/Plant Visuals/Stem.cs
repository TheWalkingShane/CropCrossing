using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    public Sprite stemImage;
    public Color stemColor; 
    public List<Sprite> coreImages; // List of core images
    public List<Core> cores = new List<Core>();
    public List<Vector3> positionOffsets = new List<Vector3>(); // List of positional offsets for cores
    private SpriteRenderer stemSpriteRenderer;
    private void Start()
    {
        // Get the component for the stem image
        stemSpriteRenderer = GetComponent<SpriteRenderer>();
        
        // Set the stem image and color
        stemSpriteRenderer.sprite = stemImage;
        

        CreateCores();
    }

    public void SetStemColor(Color newColor)
    {
        stemColor = newColor;
    }

public Sprite GetCoreImage()
{
    if (coreImages.Count > 0)
    {
        return coreImages[0];
    }
    else
    {
        // Return a default sprite or handle the case where coreImages is empty.
        return null; // Change this to your default sprite or handle the case accordingly.
    }
}
    
    
    private void CreateCores()
    {
        for (int i = 0; i < positionOffsets.Count; i++)
        {
            Vector3 offset = positionOffsets[i];

            // Instantiate a new core
            Core newCore = Instantiate(cores[0], transform.position + offset, Quaternion.identity);
            
            // Set core properties based on stem traits
            newCore.SetCoreAttributes(coreImages[i], stemColor);
            
            // Add the core to the list
            cores.Add(newCore);
        }
    } 
}
