using TMPro; 
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    
   
    
    public GameObject shopCanvas; 
    public TextMeshProUGUI moneyHUD; 
    public int money = 1000; 
    
    public GameObject inventorySlotPrefab;
    public Transform inventoryGrid;
    // to update
    
    public int[] prices = {10, 20, 30, 40}; // Prices for each item
    
    // An array to hold the max purchases for each individual item.
    public int[] maxPurchasesPerItem = {5, 5, 5, 1}; 
    
    // An array to keep track of how many times each item has been purchased.
    private int[] currentPurchases = {0, 0, 0, 0}; 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shopCanvas.SetActive(!shopCanvas.activeSelf);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMoney(50); 
        }
        
        moneyHUD.text = "Money: $" + money;
    }

    public void PurchaseItem(int index)
    {
        if (currentPurchases[index] < maxPurchasesPerItem[index] && money >= prices[index])
        {
            money -= prices[index];
            currentPurchases[index]++;
            
            // Handle the functionality for each purchase here.
            switch (index)
            {
                case 0: // Action for the music button
                    ActivateMusic();
                    break;
                
                case 1: // Action for increasing inventory size
                    IncreaseInventorySize();
                    break;
                
                case 2: // Action for increasing crop plot size
                    IncreaseCropPlotSize();
                    break;
                
                case 3: // Action for increasing the season timer
                    IncreaseSeasonTimer();
                    break;
                
                default:
                    Debug.LogError("Invalid item index");
                    break;
            }
        }
        else if (money < prices[index])
        {
            // Show a popup that the player doesn't have enough money
        }
    }

    void AddMoney(int amount)
    {
        money += amount;
    }
    
    
    void ActivateMusic()
    {
        // Pseudocode for activating music
        // For example:
        // audioSource.Play();
    }

    void IncreaseInventorySize()
    {
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryGrid);
        // Pseudocode for increasing inventory size
        // For example:
        // playerInventory.maxItems += 5;
    }

    void IncreaseCropPlotSize()
    {
        // Pseudocode for increasing crop plot size
        // For example:
        // cropPlotManager.AddNewPlot();
    }

    void IncreaseSeasonTimer()
    {
        // Pseudocode for increasing season timer
        // For example:
        // seasonManager.AddExtraTime(120);  // adds 120 seconds
    }
}