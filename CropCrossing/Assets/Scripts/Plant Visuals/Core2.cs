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