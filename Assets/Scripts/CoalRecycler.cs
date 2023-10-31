using TMPro;
using UnityEngine;

public class CoalRecycler : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private TMP_Text consumingResourceCount;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding coalRecycler;
    private Inventory playerInventory;

    private void Start()
    {
        coalRecycler = new ProductionBuilding("CoalRecycler","Sulfur", 8, 2, 1000, "Coal", false);
    }
    private void Update()
    {
        countText.text = coalRecycler.ProductionCount.ToString();
        buildingCostText.text = coalRecycler.UpgradingCost.ToString();
        consumingResourceCount.text = coalRecycler.ConsumingResourceInStorage.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        GetConsumingResource();
        if (coalRecycler.IsBuilded)
        {
            coalRecycler.GiveResources(playerInventory, coalRecycler.ResourceName, coalRecycler.ProductionCount);
            if (playerInventory.CanPay(coalRecycler.UpgradingCost))
            {
                playerInventory.Pay(coalRecycler.UpgradingCost);
                Upgrade();
            }
        }
        else if (!coalRecycler.IsBuilded && playerInventory.CanPay(coalRecycler.UpgradingCost))
        {
            playerInventory.Pay(coalRecycler.UpgradingCost);
            playerInventory.CreateInInventory(coalRecycler.ResourceName, coalRecycler.ProductionCount);
            Build();
        }
    }
    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(coalRecycler.Recycling());
        coalRecycler.Build();
    }

    private void Upgrade()
    {
        coalRecycler.Upgrade();
    }

    private void GetConsumingResource()
    {
        if (playerInventory.ResourceCreated(coalRecycler.ConsumingResourceName))
        {
            int resourceToTransfer = playerInventory.GetResourceCount(coalRecycler.ConsumingResourceName);
            playerInventory.RemoveFromInventory(coalRecycler.ConsumingResourceName, resourceToTransfer);
            coalRecycler.ConsumingResourceInStorage += resourceToTransfer;
        }
    }
}
