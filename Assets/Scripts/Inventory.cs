using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text lumberText;
    [SerializeField] private TMP_Text metalOreText;
    [SerializeField] private TMP_Text coalText;
    [SerializeField] private TMP_Text buildingMaterialsText;
    [SerializeField] private TMP_Text sulfurText;
    [SerializeField] private TMP_Text metalIngotText;
    [SerializeField] private Canvas inventoryScreen;
    private Dictionary<string,int> inventory = new Dictionary<string,int>();
    private int startingCoinCount = 1;

    private void Start()
    {
        CreateInInventory("Coin", startingCoinCount);
        inventoryScreen.enabled = false;
    }

    private void Update()
    {
        inventory.TryGetValue("Coin", out int coinCount);
        coinText.text = coinCount.ToString();
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryScreen.enabled = !inventoryScreen.enabled;
        }
    }

    public void Pay(int cost)
    {
        inventory["Coin"] -= cost;
    }

    public bool CanPay(int cost)
    {
        if (inventory["Coin"] >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CreateInInventory(string resourceName, int resourceCount)
    {
        inventory.Add(resourceName, resourceCount);
        ShowResources();
    }

    public void AddToInventory(string resourceName, int resourceCount)
    {
        inventory[resourceName] += resourceCount;
        ShowResources();
    }

    public void RemoveFromInventory(string resourceName,int resourceCount)
    {
        inventory[resourceName]-=resourceCount;
        ShowResources();
    }

    public int GetResourceCount(string resourceName)
    {
        return inventory[resourceName];
    }

    public bool ResourceCreated (string resourceName)
    {
        return inventory.TryGetValue(resourceName, out int value);
    }

    private void ShowResources()
    {
        inventory.TryGetValue("Lumber", out int lumberCount);
        inventory.TryGetValue("MetalOre", out int metalOreCount);
        inventory.TryGetValue("Coal", out int coalCount);
        inventory.TryGetValue("BuildingMaterials", out int buildingMaterialsCount);
        inventory.TryGetValue("Sulfur", out int sulfurCount);
        inventory.TryGetValue("MetalIngot", out int metalIngotCount);
        lumberText.text = lumberCount.ToString();
        metalOreText.text = metalOreCount.ToString();
        coalText.text = coalCount.ToString();
        buildingMaterialsText.text=buildingMaterialsCount.ToString();
        sulfurText.text = sulfurCount.ToString();
        metalIngotText.text = metalIngotCount.ToString();
    }
}
