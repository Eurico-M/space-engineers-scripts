public Program()
{
}

public void Save()
{
}

public int sumItems(string itemName, string itemTypeName, IMyInventory inventory) {
    
    MyFixedPoint fixedPoint;
    MyItemType itemType = new MyItemType(itemTypeName, itemName);
    fixedPoint = inventory.GetItemAmount(itemType);
    int intValue = fixedPoint.ToIntSafe();
    return intValue;
}

public int currentAmmo(string[] containerNames, string itemName) {
    int currentAmmoCount = 0;
    string itemType = "MyObjectBuilder_AmmoMagazine";

    foreach (string containerName in containerNames) {
        
        var container = GridTerminalSystem.GetBlockWithName(containerName) as IMyCargoContainer;
        int numberInventories = blockName.InventoryCount;

        for (int i = 0; i < numberInventories; i++) {            
            IMyInventory inventory = blockName.GetInventory(i);
            int itemCount = sumItems(itemName, itemType, inventory);
            currentAmmoCount += itemCount;
        }
    };

    return currentAmmoCount;
}

public void Main(string argument, UpdateType updateSource) {

    // SET THESE:
    string[] ammoTypeA_ContainerNames = {
        "container 1 Name",
        "container 2 Name"
    };

    string[] ammoTypeB_ContainerNames = {
        "container 3 Name",
        "container 4 Name"
    };

    Dictionary<string,int> requiredAmmoAmounts = {
        {"MediumCalibreAmmo", 100},
        {"LargeCalibreAmmo", 100}
    }
    // STOP SETTING
    

    int ammoTypeA_current = currentAmmo(ammoTypeA_ContainerNames, "MediumCalibreAmmo");
    int ammoTypeB_current = currentAmmo(ammoTypeB_ContainerNames, "LargeCalibreAmmo");

    

    
        


}