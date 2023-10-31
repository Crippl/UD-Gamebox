using UnityEngine;

public class Market : MonoBehaviour
{
    private Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        GiveCoinForResource("Lumber", 1);
        GiveCoinForResource("MetalOre", 2);
        GiveCoinForResource("Coal", 3);
        GiveCoinForResource("BuildingMaterials", 4);
        GiveCoinForResource("Sulfur", 5);
        GiveCoinForResource("MetalIngot", 15);
    }

    private void GiveCoinForResource(string resourceName,int marketPrice)
    {
        if (playerInventory.ResourceCreated(resourceName))
        {
            int lumberCountInInventory = playerInventory.GetResourceCount(resourceName);
            playerInventory.RemoveFromInventory(resourceName, lumberCountInInventory);
            int depositCoin = lumberCountInInventory * marketPrice;
            playerInventory.AddToInventory("Coin", depositCoin);
        }
    }
}
