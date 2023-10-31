using System.Collections;
using UnityEngine;

[System.Serializable]
public class ProductionBuilding
{
    public string BuildingName { get; set; }
    public string ResourceName { get; set; }
    public int ProducingTime { get; set; }
    public int ProductionCount { get; set; }
    public int BuildingLevel { get;set; }
    public int ProductionPerCycle { get; set; }
    public int UpgradingCost { get; set; }
    public string ConsumingResourceName { get; set; }
    public int ConsumingResourceCount { get; set; }
    public int RecyclingTime { get; set; }
    public int RecyclingPerCycle { get; set; }
    public int ConsumingResourceInStorage { get; set; }
    public bool IsBuilded { get; set; }

    public ProductionBuilding() { }

    public ProductionBuilding(string buildingName, string resourceName, int producingTime, int productionPerCycle, int upgradingCost, bool isBuilded)
    {
        this.BuildingName = buildingName;
        this.ResourceName = resourceName;
        this.ProducingTime = producingTime;
        this.ProductionPerCycle = productionPerCycle;
        this.UpgradingCost = upgradingCost;
        this.IsBuilded = isBuilded;
    }

    public ProductionBuilding(string buildingName, string resourceName,int recyclingTime,int recyclingPerCycle,int upgradingCost,string consumingResourceName,bool isBuilded)
    {
        this.BuildingName = buildingName;
        this.ResourceName = resourceName;
        this.RecyclingTime = recyclingTime;
        this.RecyclingPerCycle = recyclingPerCycle;
        this.UpgradingCost = upgradingCost;
        this.ConsumingResourceName = consumingResourceName;
        this.IsBuilded= isBuilded;
    }

    public void Build()
    {
        IsBuilded = true;
        BuildingLevel++;
        RaiseBuildingCost();
        RaiseRecyclingConsume();
    }

    public void Upgrade()
    {
        BuildingLevel++;
        RaiseBuildingCost();
        RaiseRecyclingConsume();
    }

    public IEnumerator Production()
    {
        while (true)
        {
            yield return new WaitForSeconds(ProducingTime);
            ProductionCount += (ProductionPerCycle * BuildingLevel);
        }
    }

    public IEnumerator Recycling()
    {
        while (true)
        {
            yield return new WaitForSeconds(RecyclingTime);
            if (ConsumingResourceInStorage >= RecyclingPerCycle)
            {
                ConsumingResourceInStorage -= RecyclingPerCycle;
                ProductionCount += ((RecyclingPerCycle * BuildingLevel));
            }
        }
    }
    public void GiveResources(Inventory inventory,string resourceName,int productionCount)
    {
        inventory.AddToInventory(resourceName, productionCount);
        ProductionCount-=productionCount;
    }

    private void RaiseBuildingCost()
    {
        UpgradingCost *= BuildingLevel;
    }

    private void RaiseRecyclingConsume()
    {
        RecyclingPerCycle *= BuildingLevel;
    }
}
