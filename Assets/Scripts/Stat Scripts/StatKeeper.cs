using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatKeeper : MonoBehaviour
{
    //this entire script is solely for the purpose of keeping the stats tracked of each crop.
    List<string> genes = new List<string>();
    //String representations of gene sequences that influence the stats of the crop, A corresponds to nutrition
    
    //B corresponds to Firmness
    
    //C corresponds to yield
    
    //D corresponds to lifespan
    
    
    //the actual stats of the crop
    public float nutrition = 20f;
    public float yield = 3f;
    public float firmness = 15f;
    public float lifespan = 5f;
    public float money = 5f;
    
    public string Agenes = "AaAA";
    public string Bgenes = "Bbbb";
    public string Cgenes = "CcCc";
    public string Dgenes = "dddd";

    public void Awake()
    {
        genes.Add(Agenes);
        genes.Add(Bgenes);
        genes.Add(Cgenes);
        genes.Add(Dgenes);
    }

    public List<string> Retrieve_Genes()
    {
        return genes;
    }

    public void Set_New_Stats(float newNutrition, float newYield, float newFirmness, float newLifespan)
    {
        nutrition += newNutrition;
        yield += newYield;
        firmness += newFirmness;
        lifespan += newLifespan;
    }
    
    public void SetNewGenes(List<string> newGenes)
    {
        genes.Clear();
        
        for(int i = 0; i < 4; i++) 
        {
            genes.Add(newGenes[i]);
        }
    }
    
    public float[] GetStats()
    {
        float[] statArray = new float[4]{nutrition, yield, firmness, lifespan};
        return statArray;
    }

    public float GetMoney()
    {
        return money;
    }

    public void setMoney(float newMoney)
    {
        money = newMoney;
    }
    //this initial stats are completely arbitrary for the time being.
    //monetary value is deliberately absent and will likely only be incorporated once the money exists in the first place.
}
