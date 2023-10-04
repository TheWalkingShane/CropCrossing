using UnityEngine;
using TMPro;

public class CropHoverInfo : MonoBehaviour
{
    public GameObject offSET; // Reference to the info box GameObject
    public bool isHovering = false; // Flag to track if the mouse is hovering
    public InfoUpdater infoBox;
    private Camera cam;
    private StatKeeper statKeeper; // Reference to the StatKeeper script
    private TextMeshProUGUI nutrientsText;
    private TextMeshProUGUI yieldText;
    private TextMeshProUGUI firmnessText;
    private TextMeshProUGUI lifespanText;
    private TextMeshProUGUI valueText;

    private void Start()
    {
        cam = Camera.main;
        // Ensure the info box is initially hidden
        if (offSET != null)
        {
            offSET.SetActive(false);
        }
    }

    private void Update()
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object or any of its children have a Stem2 component
            Stem2[] stem2Components = hit.transform.GetComponentsInChildren<Stem2>();
            if (stem2Components.Length > 0)
            {
                // Mouse is hovering over an object with a Stem2 component
                isHovering = true;

                // Show the info box
                if (offSET != null)
                {
                    offSET.SetActive(true);

                    // Make the info box follow the mouse
                    Vector3 mousePos = Input.mousePosition;
                    float offsetY = offSET.transform.localScale.y * 2f; // Adjust this value as needed
                    offSET.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y - offsetY, 5));
                }

                // Get the StatKeeper component from the hit object
                statKeeper = hit.transform.GetComponentInChildren<StatKeeper>();
                Debug.Log("Checking for Statkeeper in " + hit.transform.gameObject.name);
                // Check if StatKeeper exists
                if (statKeeper != null)
                {
                    Debug.Log("Stat keeper found");
                    // Get the data values
                    float nutrition = statKeeper.nutrition;
                    float yield = statKeeper.yield;
                    float firmness = statKeeper.firmness;
                    float lifespan = statKeeper.lifespan;
                    float money = statKeeper.money;

                    // Assign the values to the text elements in your info box
                    infoBox.UpdateCropInfoText(nutrition, yield, firmness, lifespan, money);
                }
            }
            else
            {
                // Mouse is not hovering over an object with a Stem2 component
                isHovering = false;

                // Hide the info box
                if (offSET != null)
                {
                    offSET.SetActive(false);
                }
            }
        }
        else
        {
            isHovering = false;
            offSET.SetActive(false);
        }
    }
    
}