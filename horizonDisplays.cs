public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

public void Save()
{
}

string[] reactorDisplays = {
    "Reactor"
};
string[] ammoDisplays = {
    "Control Seat"
};
string[] fuelNames = {
    "Uranium"
};
string[] fuelDisplayNames = {
    "Fuel"
};
string[] ammoNames = {
    "NATO_25x184mm",
    "MediumCalibreAmmo",
    "LargeCalibreAmmo"
};
string[] ammoDisplayNames = {
    "Gatling Ammo",
    "Cannon Ammo",
    "Artillery Ammo"
};
string fuelType = "MyObjectBuilder_Ingot";
string ammoType = "MyObjectBuilder_AmmoMagazine";

int[] fuelSums = new int[fuelNames.Length];
int[] ammoSums = new int[ammoNames.Length];

public getSums(string[] names, string type, int[] sums) {
    

public List<IMyTerminalBlock> blocksInGrid = new List<IMyTerminalBlock>();
GridTerminalSystem.GetBlocksOfType(blocksInGrid, block => block.IsSameConstructAs(Me));

public int sumItems(string itemName, string itemTypeName, IMyInventory inventory) {
    MyFixedPoint fixedPoint;
    MyItemType itemType = new MyItemType(itemTypeName, itemName);
    fixedPoint = inventory.GetItemAmount(itemType);
    int intValue = fixedPoint.ToIntSafe();
    return intValue;
}


public void Main(string argument, UpdateType updateSource)
{
    
}