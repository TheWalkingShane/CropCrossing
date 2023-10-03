using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/New Ship Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public int cost;
    public GameObject cropGO;
}
