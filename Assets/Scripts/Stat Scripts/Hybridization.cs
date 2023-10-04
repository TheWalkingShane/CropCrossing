using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Hybridization : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject finalSlot;
    public GameObject hybridPlantDragPrefab;
    public GameObject hybridPlant;

    public GameObject leftCrop;
    public GameObject rightCrop;
    public List<string> slot1Genes;
    public List<string> slot2Genes;

    public List<string> hybridGenes = new List<string>();
    List<string> traitsList = new List<string>() { "Aa", "Bb", "Cc", "Dd" };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("space") && (slot1Crop.transform.childCount != 0 && slot2Crop.transform.childCount != 0) &&
        //     finalSlot.transform.childCount == 0)
        // {
        //     slot1Genes = slot1Crop.transform.GetChild(0).GetComponent<StatKeeper>().Retrieve_Genes();
        //     slot2Genes = slot2Crop.transform.GetChild(0).GetComponent<StatKeeper>().Retrieve_Genes();
        //     GenerateGeneSequences();
        // }
    }

    void GenerateGeneSequences()
    {
        for(int i = 0; i < 4; i++)
        {
            List<string> permutations = Permutate(traitsList[i]);
            RemoveInvalidCombos(slot1Genes[i], slot2Genes[i], permutations);
            GeneSelection(permutations, traitsList[i]);
        }
        
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(hybridGenes[i] + "\n");
        }
        
        CreateNewHybrid();
    }
    
    static List<string> Permutate(string characters)
    {
        List<string> permutations = new List<string>();
        char[] current = new char[4];
        GeneratePermutationsHelper(characters, 0, current, permutations);
        return permutations;
    }

    static void GeneratePermutationsHelper(string characters, int currentIndex, char[] current, List<string> permutations)
    {
        if (currentIndex == 4)
        {
            permutations.Add(new string(current));
            return;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            current[currentIndex] = characters[i];
            GeneratePermutationsHelper(characters,currentIndex + 1, current, permutations);
        }
    }

    void GeneSelection(List<string> permutations, string trait)
    {
        int geneWeight = 1, currentWeight = 0;
        List<Tuple<int, string>> weightedGenes = new List<Tuple<int, string>>();
        
        foreach(string sequence in permutations)
        {
            foreach (char gene in sequence)
            {
                if (gene == trait[0])
                {
                    geneWeight += 1;
                }
                else
                {
                    geneWeight -= 1;
                }
            }

            if (geneWeight < 1)
            {
                geneWeight = 1;
            }
            weightedGenes.Add(new Tuple<int, string>(geneWeight, sequence));
            geneWeight = 1;
        }
        
        List<string> genesToBeSelected = new List<string>();
        
        weightedGenes.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        
        foreach(Tuple<int, string> pair in weightedGenes)
        {
            currentWeight += pair.Item1;
            for (int i = 0; i < currentWeight; i++)
            {
                genesToBeSelected.Add(pair.Item2);
            }
        }
        
        
        int geneChooser = Random.Range(0, genesToBeSelected.Count);
        hybridGenes.Add(genesToBeSelected[geneChooser]);
    }

    void RemoveInvalidCombos(string gene1, string gene2, List<string> permutations)
    {
        int[] sameIndex = new int[4];

        for (int i = 0; i < 4; i++)
        {
            if (gene1[i] == gene2[i])
            {
                sameIndex[i] = 1;
            }
            else
            {
                sameIndex[i] = 0;
            }
        }

        for (int i = permutations.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < 4; j++)
            {
                if (sameIndex[j] == 1)
                {
                    if (permutations[i][j] != gene1[j])
                    {
                        permutations.Remove(permutations[i]);
                        break;
                    }
                }
            }
        }
    }

    void CreateNewHybrid()
    {
        GameObject hybrid = finalSlot.transform.GetChild(0).gameObject;

        float newNutrition = 0, newYield = 0, newFirmness = 0, newLifespan = 0;

        float[] statArray = hybrid.GetComponent<StatKeeper>().GetStats();

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                foreach (var gene in hybridGenes[0])
                {
                    if (gene == 'A')
                    {
                        newNutrition += 5;
                    }
                    else
                    {
                        newNutrition -= 5;
                    }
                    
                }
            }
            
            if (i == 1)
            {
                foreach (var gene in hybridGenes[1])
                {
                    if (gene == 'B')
                    {
                        newYield += 2;
                    }
                    else
                    {
                        newYield -= 2;
                    }
                    
                }
            }
            
            if (i == 2)
            {
                foreach (var gene in hybridGenes[2])
                {
                    if (gene == 'C')
                    {
                        newFirmness += 3;
                    }
                    else
                    {
                        newFirmness -= 3;
                    }
                    
                }
            }
            
            if (i == 3)
            {
                foreach (var gene in hybridGenes[3])
                {
                    if (gene == 'D')
                    {
                        newLifespan += 1;
                    }
                    else
                    {
                        newLifespan -= 1;
                    }
                    
                }
            }
        }
        
        hybrid.GetComponent<StatKeeper>().Set_New_Stats(newNutrition, newYield, newFirmness, newLifespan);
        hybrid.GetComponent<StatKeeper>().SetNewGenes(hybridGenes);
    }

    void SeasonChange()
    {
        
    }

    void LeftSlotFilled(GameObject slotObject)
    {
        leftCrop = slotObject.transform.GetChild(0).gameObject;
        this.SendMessage("SetObject1", leftCrop);
    }
    
    void RightSlotFilled(GameObject slotObject)
    {
        rightCrop = slotObject.transform.GetChild(0).gameObject;
        this.SendMessage("SetObject2", rightCrop);
        CheckIfReadyToHybridize();
    }

    void CheckIfReadyToHybridize()
    {
        if (leftCrop != null && rightCrop != null)
        {
            BeginHybridization();
        }
    }
    
    void BeginHybridization()
    {
        GameObject dragObject = Instantiate(hybridPlantDragPrefab, finalSlot.transform);
        this.gameObject.SendMessage("setDragObject", dragObject);
        this.gameObject.SendMessage("CombineObjects");
    }
}    
