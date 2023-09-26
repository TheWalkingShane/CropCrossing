using UnityEngine;

public class Core2 : MonoBehaviour
{
    private GameObject connectionPoint; // The 3D parent object tracking the connection point

    // Set core properties based on stem traits
    public void SetCoreAttributes(Color color, Vector3 size, Vector3 rotation, Sprite image, GameObject connection)
    {
        GetComponent<SpriteRenderer>().color = color;
        transform.localScale = size;
        transform.localRotation = Quaternion.Euler(rotation);
        GetComponent<SpriteRenderer>().sprite = image;
        
        connectionPoint = connection;
        transform.SetParent(connectionPoint.transform); // Set the connection point as the parent
    }
}