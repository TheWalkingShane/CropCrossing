using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    public Transform slottedItem;

    void Start()
    {
        slottedItem = null;
    }
    
    public void Insert_Item(Transform item)
    {
        slottedItem = item;
    }
    
    public void Remove_Item()
    {
        slottedItem = null;
    }
}
