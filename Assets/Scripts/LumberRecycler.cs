using TMPro;
using UnityEngine;

public class LumberRecycler : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private TMP_Text consumingResourceCount;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding lumberRecycler;
    private Inventory playerInventory;

    private void Start()
    {
        lumberRecycler = new ProductionBuilding("LumberRecycler","BuildingMaterials", 5, 2, 100, "Lumber",false);
    }
    private void Update()
    {
        countText.text = lumberRecycler.ProductionCount.ToString();
        buildingCostText.text = lumberRecycler.UpgradingCost.ToString();
        consumingResourceCount.text=lumberRecycler.ConsumingResourceInStorage.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        GetConsumingResource();
        if (lumberRecycler.IsBuilded)
        {
            lumberRecycler.GiveResources(playerInventory, lumberRecycler.ResourceName, lumberRecycler.ProductionCount);
            if (playerInventory.CanPay(lumberRecycler.UpgradingCost))
            {
                playerInventory.Pay(lumberRecycler.UpgradingCost);
                Upgrade();
            }
        }
        else if (!lumberRecycler.IsBuilded && playerInventory.CanPay(lumberRecycler.UpgradingCost))
        {
            playerInventory.Pay(lumberRecycler.UpgradingCost);
            playerInventory.CreateInInventory(lumberRecycler.ResourceName, lumberRecycler.ProductionCount);
            Build();
        }
    }
    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(lumberRecycler.Recycling());
        lumberRecycler.Build();
    }

    private void Upgrade()
    {
        lumberRecycler.Upgrade();
    }

    private void GetConsumingResource()
    {
        if (playerInventory.ResourceCreated(lumberRecycler.ConsumingResourceName))
        {
            int resourceToTransfer = playerInventory.GetResourceCount(lumberRecycler.ConsumingResourceName);
            playerInventory.RemoveFromInventory(lumberRecycler.ConsumingResourceName, resourceToTransfer);
            lumberRecycler.ConsumingResourceInStorage += resourceToTransfer;
        }
    }
}
