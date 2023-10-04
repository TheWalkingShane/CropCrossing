using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Hybridization : MonoBehaviour
{
    public GameObject hybrid;
    public List<string> slot1Genes;
    public List<string> slot2Genes;

    public List<string> hybridGenes = new List<string>();
    List<char> traitsList = new List<char>();
    private List<string> permutationList = new List<string>() { "Aa", "Bb", "Cc", "Dd" };
    
    public char nutritionChar = 'A', yieldChar = 'B', firmChar = 'C', lifeChar = 'D';
    public TextMeshProUGUI nGene, yGene, fGene, lGene;
    private string currentSeason = "Summer";
    
    public ShopManager moneyKeeper;

    public void BeginHybridProcess(GameObject crop1, GameObject crop2, GameObject hybridCrop)
    {
        slot1Genes = crop1.GetComponent<StatKeeper>().Retrieve_Genes();
        slot2Genes = crop2.GetComponent<StatKeeper>().Retrieve_Genes();
        hybrid = hybridCrop;
        
        traitsList.Add(nutritionChar);
        traitsList.Add(yieldChar);
        traitsList.Add(firmChar);
        traitsList.Add(lifeChar);
        // for(int i = 0; i < 4; i++) 
        // {
        //     Debug.Log(slot1Genes[i] + "\n");
        // }
        //
        // for(int i = 0; i < 4; i++) 
        // {
        //     Debug.Log(slot2Genes[i] + "\n");
        // }
        GenerateGeneSequences();
    }
    void GenerateGeneSequences()
    {
        for(int i = 0; i < 4; i++)
        {
            List<string> permutations = Permutate(permutationList[i]);
            RemoveInvalidCombos(slot1Genes[i], slot2Genes[i], permutations);
            GeneSelection(permutations, traitsList[i]);
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

    void GeneSelection(List<string> permutations, char trait)
    {
        int geneWeight = 1, currentWeight = 0;
        List<Tuple<int, string>> weightedGenes = new List<Tuple<int, string>>();
        
        foreach(string sequence in permutations)
        {
            foreach (char gene in sequence)
            {
                if (gene == trait)
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
        float newNutrition = 0, newYield = 0, newFirmness = 0, newLifespan = 0;
        StatKeeper hybridStats = hybrid.GetComponent<StatKeeper>();
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                foreach (var gene in hybridGenes[0])
                {
                    if (gene == nutritionChar)
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
                    if (gene == yieldChar)
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
                    if (gene == firmChar)
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
                    if (gene == lifeChar)
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
        
        hybridStats.Set_New_Stats(newNutrition, newYield, newFirmness, newLifespan);
        hybridStats.SetNewGenes(hybridGenes);
        
        float hybridMoney = hybridStats.GetMoney();
        float amount = newNutrition + newYield + newFirmness + newLifespan;
        float newMoneyAmount = hybridMoney + amount;
        
        hybridStats.setMoney(newMoneyAmount);
        moneyKeeper.AddMoney((int)newMoneyAmount);
        
        hybridGenes.Clear();
    }

    void SeasonChange()
    {
        if (currentSeason == "Spring")
        {
            currentSeason = "Summer";
            nutritionChar = 'A';
            yieldChar = 'B';
            firmChar = 'C';
            lifeChar = 'D';
        } 
        else if (currentSeason == "Summer")
        {
            currentSeason = "Fall";
            nutritionChar = 'A';
            yieldChar = 'b';
            firmChar = 'C';
            lifeChar = 'd';
        } 
        else if (currentSeason == "Fall")
        {
            currentSeason = "Winter";
            nutritionChar = 'a';
            yieldChar = 'B';
            firmChar = 'c';
            lifeChar = 'D';
        }
        else if (currentSeason == "Winter")
        {
            currentSeason = "Spring";
            nutritionChar = 'a';
            yieldChar = 'b';
            firmChar = 'c';
            lifeChar = 'd';
        }
        
        nGene.SetText("Nutrition: " + nutritionChar);
        yGene.SetText("Yield: " + yieldChar);
        fGene.SetText("Firmness: " + firmChar);
        lGene.SetText("Lifespan: " + lifeChar);
    }
    
}    
