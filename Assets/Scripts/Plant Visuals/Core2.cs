using UnityEngine;

public class Core2 : MonoBehaviour
{
    private GameObject connectionPoint; // The 3D parent object tracking the connection point

    // Set core properties based on stem traits
    public void SetCoreAttributes(Color color, Vector3 size, Vector3 rotation, Sprite image, GameObject connection)
    {
        transform.localScale = size;
        transform.localRotation = Quaternion.Euler(rotation);
        GetComponent<SpriteRenderer>().sprite = image;
        connectionPoint = connection;
        GetComponent<SpriteRenderer>().color = color;
        transform.SetParent(connectionPoint.transform); // Set the connection point as the parent
        
        //The above function is mostly outdated and handled by Stem2 code.
    }
    
    private void CheckForParentStem2(Transform currentTransform)
    {
        // Check if the current transform has a parent
        if (currentTransform.parent != null)
        {
            // Check if the parent has a Stem2 component
            Stem2 stem2Component = currentTransform.parent.GetComponent<Stem2>();
            if (stem2Component != null)
            {
                // Parent has a Stem2 component
                
            }
            else
            {
                // Recursively check the next parent
                CheckForParentStem2(currentTransform.parent);
            }
        }
        else
        {
            // No more parents to check
            Debug.Log("No parent with Stem2 component found.");
        }
    }

    public void SetCoreColor(Color CoreColor)
    {
        //This function shall set core color
        SpriteRenderer[] childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer childRenderer in childSpriteRenderers)
        {
            childRenderer.color = CoreColor;
        }
    }
}