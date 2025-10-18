public Program()
{
}

public void Save()
{
}

// Sum all items with given itemName and itemTypeName that are present in an inventory
public int sumItems(string itemName, string itemTypeName, IMyInventory inventory) {    
    MyFixedPoint fixedPoint;
    MyItemType itemType = new MyItemType(itemTypeName, itemName);
    fixedPoint = inventory.GetItemAmount(itemType);
    int intValue = fixedPoint.ToIntSafe();
    return intValue;
}

// iterates through containers, summing the ammount of a specific given itemName
public int currentAmmo(string[] containerNames, string itemName) {
    int currentAmmoCount = 0;
    string itemType = "MyObjectBuilder_AmmoMagazine";

    foreach (string containerName in containerNames) {
        
        var container = GridTerminalSystem.GetBlockWithName(containerName) as IMyCargoContainer;
        int numberInventories = container.InventoryCount;

        for (int i = 0; i < numberInventories; i++) {
            IMyInventory inventory = container.GetInventory(i);
            int itemCount = sumItems(itemName, itemType, inventory);
            currentAmmoCount += itemCount;
        }
    };

    return currentAmmoCount;
}

public class AmmoType {
    public string name;
    public string[] containerNames;
    public int desiredAmount;
    public MyFixedPoint missingAmount;

    public AmmoType(string nm, string[] cNms, int dAmount) {
        name = nm;
        containerNames = cNms;
        desiredAmount = dAmount;
        missingAmount = 0;
    }
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

    int ammoTypeA_desired = 100;
    int ammoTypeB_desired = 100;

    string sourceContainerName = "source container name";

    // STOP SETTING
    
    AmmoType railgunAmmo = new AmmoType(ammoTypeA_Name, ammoTypeA_ContainerNames, ammoTypeA_desired);
    AmmoType cannonAmmo = new AmmoType(ammoTypeB_Name, ammoTypeB_ContainerNames, ammoTypeB_desired);

    List<AmmoType> ammoTypes = new List<AmmoType> {
        railgunAmmo,
        cannonAmmo
    };

    foreach (var ammo in ammoTypes) {
        ammo.missingAmount = ammo.desiredAmount - currentAmmo(ammo.containerNames, ammo.name);
    }

    foreach (var ammo in ammoTypes) {
        if (ammo.missingAmount > 0) {
            var sourceContainer = GridTerminalSystem.GetBlockWithName(sourceContainerName) as IMyCargoContainer;
            var inventory = 
        }
    }
    

    
        


}