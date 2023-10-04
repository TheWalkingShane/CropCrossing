using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUpdater : MonoBehaviour
{
    public TextMeshProUGUI Nutrients;
    public TextMeshProUGUI Yield;
    public TextMeshProUGUI Firmness;
    public TextMeshProUGUI Lifespan;
    public TextMeshProUGUI Value;
    
    public void UpdateCropInfoText(float nutrition, float yield, float firmness, float lifespan, float money)
    {
        Debug.Log("Updating Crop info");
        Nutrients.text = "Nutrition: " + nutrition.ToString();
        Yield.text = "Yield: " + yield.ToString();
        Firmness.text = "Firmness: " + firmness.ToString();
        Lifespan.text = "Lifespan: " + lifespan.ToString();
        Value.text = "Value: " + money.ToString();
    }
}
