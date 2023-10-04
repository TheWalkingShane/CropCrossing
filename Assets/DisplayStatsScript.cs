using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStatsScript : MonoBehaviour
{
    public GameObject StatTextManager;
    public GameObject GeneTextManager;
    
    private float[] _statArray;
    private List<string> _geneArray;
    private StatKeeper _thisObjectStats;

    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        _thisObjectStats = this.transform.GetChild(0).GetComponent<StatKeeper>();
        _statArray = _thisObjectStats.GetStats();
        _geneArray = _thisObjectStats.Retrieve_Genes();
        
        TextMeshProUGUI[] statChildren = StatTextManager.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI[] geneChildren = GeneTextManager.GetComponentsInChildren<TextMeshProUGUI>();

        int j = 0;
        for (int i = 1; i <= 4; i++)
        {
            statChildren[i].SetText(_statArray[j].ToString());
            geneChildren[i].SetText(_geneArray[j]);
            j++;
        }
        
        statChildren[5].SetText(_thisObjectStats.GetMoney().ToString());
    }

    private void OnMouseExit()
    {
        TextMeshProUGUI[] statChildren = StatTextManager.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI[] geneChildren = GeneTextManager.GetComponentsInChildren<TextMeshProUGUI>();

        int j = 0;
        for (int i = 1; i <= 4; i++)
        {
            statChildren[i].SetText(" ");
            geneChildren[i].SetText(" ");
            j++;
        }
        
        statChildren[5].SetText(" ");
    }

    public void setTextHolderObjects(GameObject geneObject, GameObject statObject)
    {
        StatTextManager = statObject;
        GeneTextManager = geneObject;
    }
    
}
