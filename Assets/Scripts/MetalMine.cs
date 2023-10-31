using TMPro;
using UnityEngine;

public class MetalMine : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding metalMine;
    private Inventory playerInventory;


    private void Start()
    {
        metalMine = new ProductionBuilding("MetalMine","MetalOre", 2, 1, 2,false);
    }

    private void Update()
    {
        countText.text = metalMine.ProductionCount.ToString();
        buildingCostText.text = metalMine.UpgradingCost.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        if (metalMine.IsBuilded)
        {
            metalMine.GiveResources(playerInventory, metalMine.ResourceName, metalMine.ProductionCount);
            if (playerInventory.CanPay(metalMine.UpgradingCost))
            {
                playerInventory.Pay(metalMine.UpgradingCost);
                Upgrade();
            }
        }
        else if (!metalMine.IsBuilded && playerInventory.CanPay(metalMine.UpgradingCost))
        {
            playerInventory.Pay(metalMine.UpgradingCost);
            playerInventory.CreateInInventory(metalMine.ResourceName, metalMine.ProductionCount);
            Build();
        }
    }

    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(metalMine.Production());
        metalMine.Build();
    }

    private void Upgrade()
    {
        metalMine.Upgrade();
    }
}
