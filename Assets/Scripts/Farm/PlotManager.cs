using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    private BoxCollider2D plantCollider;

    public Sprite[] plantStages;
    int plantStage = 0;
    private float timeBtwStages = 2f;
    private float timer;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;

            if (timer < 0 && plantStage <= plantStages.Length-1)
            {
                timer = timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
            if (plantStage == plantStages.Length - 1)
            {
                Harvest();    
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isPlanted)
        {
            Plant();
            Debug.Log("Clicked");
        }
        
    }

    void Harvest()
    {
        Debug.Log("Harvested");
        isPlanted = false;
        plant.gameObject.SetActive(false);
    }

    void Plant()
    {
        Debug.Log("Planted");
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
    
}
