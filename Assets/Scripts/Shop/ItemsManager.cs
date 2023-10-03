using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public Behaviour Shop_Canvas;
    public ShopManager shopManager;
    public InventorySlotQuib[] inventorySlotQuibs;
    
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchasedItem;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelGO[i].SetActive(true);
        }

        LoadPanels();
        CheckPurchaseable();

    }

    // Update is called once per frame
    void Update()
    {
        //Press S to enable and disable CropShop Canvas
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shop_Canvas.enabled = !Shop_Canvas.enabled;
        }
    }

    public void CheckPurchaseable()
    {
        //Will if the player has enough Money to buy the item
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (shopManager.money >= shopItemSO[i].cost)
            {
                myPurchasedItem[i].interactable = true;
                
            }
            else
            {
                myPurchasedItem[i].interactable = false;
            }
        }
    }

    public void LoadPanels()
    {
        //Load the scriptable object with Information
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].priceTxt.text = "Price: " + shopItemSO[i].cost.ToString();
        }
    }

    public void PurchaseItem(int btnNumber)
    {
        //Will check if User has enough Money to Purchase the Item
        if (SlotFull())
        {
            Debug.Log("Slots are Full");
        }
        else
        {
            if (shopManager.money >= shopItemSO[btnNumber].cost)
            {
                shopManager.money = shopManager.money - shopItemSO[btnNumber].cost;
                CheckPurchaseable();
                CreateCrop(btnNumber);
            }
        }

    }

    public void CreateCrop(int btnNumber)
    {
        //Spawn the crop Purchased
        for (int i = 0; i < inventorySlotQuibs.Length; i++)
        {
            if (!inventorySlotQuibs[i].isOccupied)
            {
                Instantiate(shopItemSO[btnNumber].cropGO, inventorySlotQuibs[i].transform.position, Quaternion.identity);
                break;
            }
            
        }
        
    }

    public bool SlotFull()
    {
        for (int i = 0; i < inventorySlotQuibs.Length; i++)
        {
            if (!inventorySlotQuibs[i].isOccupied)
            {
                return false;
            }
            
        }

        return true;
    }
}
