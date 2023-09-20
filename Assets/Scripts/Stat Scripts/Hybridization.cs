using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Hybridization : MonoBehaviour
{
    public GameObject slot1Crop;
    public GameObject slot2Crop;//Needed for hybrid process

    public List<string> slot1Genes;
    public List<string> slot2Genes;

    public List<string> hybridGenes = new List<string>();

    StringBuilder geneTool = new StringBuilder();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            slot1Genes = slot1Crop.GetComponent<StatKeeperB>().Retrieve_Genes();
            slot2Genes = slot2Crop.GetComponent<StatKeeper>().Retrieve_Genes();
            Hybridize();
        }
    }

    void Hybridize()
    {
        for (int i = 0; i < 4; i++)
        {
            
            for (int j = 0; j < 4; j++)
            {
                int inhrtNumber  = Random.Range(1, 10);
                
                if (inhrtNumber <= 5)
                {
                    geneTool.Append(slot1Genes[i][j]);
                }
                else
                {
                    geneTool.Append(slot2Genes[i][j]);
                }
            }
            hybridGenes.Add(geneTool.ToString());
            geneTool.Clear();
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(hybridGenes[i] + "\n");
        }
    }
}
