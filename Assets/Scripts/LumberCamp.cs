using TMPro;
using UnityEngine;

public class LumberCamp: MonoBehaviour
{
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text buildingCostText;
    [SerializeField] private GameObject connectedBuilding;
    private ProductionBuilding lumberCamp;
    private Inventory playerInventory;


    private void Start()
    {
        lumberCamp = new ProductionBuilding("LumberCamp","Lumber", 1, 1, 1,false);
    }

    private void Update()
    {
        countText.text = lumberCamp.ProductionCount.ToString();
        buildingCostText.text = lumberCamp.UpgradingCost.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInventory = other.GetComponent<Inventory>();
        if (lumberCamp.IsBuilded)
        {
            lumberCamp.GiveResources(playerInventory, lumberCamp.ResourceName, lumberCamp.ProductionCount);
            if ( playerInventory.CanPay(lumberCamp.UpgradingCost))
            {
                playerInventory.Pay(lumberCamp.UpgradingCost);
                Upgrade();
            }
        }
        else if (!lumberCamp.IsBuilded && playerInventory.CanPay(lumberCamp.UpgradingCost))
        {
            playerInventory.Pay(lumberCamp.UpgradingCost);
            playerInventory.CreateInInventory(lumberCamp.ResourceName, lumberCamp.ProductionCount);
            Build();
        }
    }

    private void Build()
    {
        connectedBuilding.SetActive(true);
        StartCoroutine(lumberCamp.Production());
        lumberCamp.Build();
    }

    private void Upgrade()
    {
        lumberCamp.Upgrade();
    }
}
