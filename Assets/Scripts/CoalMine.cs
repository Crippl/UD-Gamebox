using TMPro;
using UnityEngine;

public class CoalMine : MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding coalMine;
    private Inventory playerInventory;


    private void Start()
    {
        coalMine = new ProductionBuilding("CoalMine","Coal", 3, 2, 4,false);
    }

    private void Update()
    {
        countText.text = coalMine.ProductionCount.ToString();
        buildingCostText.text = coalMine.UpgradingCost.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        if (coalMine.IsBuilded)
        {
            coalMine.GiveResources(playerInventory, coalMine.ResourceName, coalMine.ProductionCount);
            if (playerInventory.CanPay(coalMine.UpgradingCost))
            {
                playerInventory.Pay(coalMine.UpgradingCost);
                Upgrade();
            }
        }
        else if (!coalMine.IsBuilded && playerInventory.CanPay(coalMine.UpgradingCost))
        {
            playerInventory.Pay(coalMine.UpgradingCost);
            playerInventory.CreateInInventory(coalMine.ResourceName, coalMine.ProductionCount);
            Build();
        }
    }
        
    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(coalMine.Production());
        coalMine.Build();
    }

    private void Upgrade()
    {
        coalMine.Upgrade();
    }
}
