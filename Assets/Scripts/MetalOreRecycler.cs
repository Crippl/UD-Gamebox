using TMPro;
using UnityEngine;

public class MetalOreRecycler : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private TMP_Text consumingResourceCount;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding metalOreRecycler;
    private Inventory playerInventory;

    private void Start()
    {
        metalOreRecycler = new ProductionBuilding("MetalOreRecycler","MetalIngot", 15, 1, 10000, "MetalOre", false);
    }
    private void Update()
    {
        countText.text = metalOreRecycler.ProductionCount.ToString();
        buildingCostText.text = metalOreRecycler.UpgradingCost.ToString();
        consumingResourceCount.text = metalOreRecycler.ConsumingResourceInStorage.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        GetConsumingResource();
        if (metalOreRecycler.IsBuilded)
        {
            metalOreRecycler.GiveResources(playerInventory, metalOreRecycler.ResourceName, metalOreRecycler.ProductionCount);
            if (playerInventory.CanPay(metalOreRecycler.UpgradingCost))
            {
                playerInventory.Pay(metalOreRecycler.UpgradingCost);
                Upgrade();
            }
        }
        else if (!metalOreRecycler.IsBuilded && playerInventory.CanPay(metalOreRecycler.UpgradingCost))
        {
            playerInventory.Pay(metalOreRecycler.UpgradingCost);
            playerInventory.CreateInInventory(metalOreRecycler.ResourceName, metalOreRecycler.ProductionCount);
            Build();
        }
    }
    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(metalOreRecycler.Recycling());
        metalOreRecycler.Build();
    }

    private void Upgrade()
    {
        metalOreRecycler.Upgrade();
    }

    private void GetConsumingResource()
    {
        if (playerInventory.ResourceCreated(metalOreRecycler.ConsumingResourceName))
        {
            int resourceToTransfer = playerInventory.GetResourceCount(metalOreRecycler.ConsumingResourceName);
            playerInventory.RemoveFromInventory(metalOreRecycler.ConsumingResourceName, resourceToTransfer);
            metalOreRecycler.ConsumingResourceInStorage += resourceToTransfer;
        }
    }
}
