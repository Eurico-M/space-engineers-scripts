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
    string ammoTypeA_Name = "MediumCalibreAmmo";
    string ammoTypeB_Name = "LargeCalibreAmmo";

    string[] ammoTypeA_ContainerNames = {
        "container 1 Name",
        "container 2 Name"
    };

    string[] ammoTypeB_ContainerNames = {
        "container 3 Name",
        "container 4 Name"
    };

    Dictionary<string,int> requiredAmmoAmounts = {
        {ammoTypeA_Name, 100},
        {ammoTypeB_Name, 100}
    }
    // STOP SETTING
    

    int ammoTypeA_current = currentAmmo(ammoTypeA_ContainerNames, ammoTypeA_Name);
    int ammoTypeB_current = currentAmmo(ammoTypeB_ContainerNames, ammoTypeB_Name);

    

    
        


}