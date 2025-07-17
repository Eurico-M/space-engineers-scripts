public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{   
    string[] arrayContainers = 
    {
        "Aurum - Cargo Container 1", 
        "Aurum - Cargo Container 2", 
        "Aurum - Cargo Container 3", 
        "Aurum - Cargo Container 4",
        "Aurum - Cargo Container 5",
        "Aurum - Cargo Container 6",
        "Aurum - Cargo Container 7",
        "Aurum - Cargo Container 8",
        "Aurum - Cargo Container 9",
        "Aurum - Cargo Container 10"
    };

    string[] arrayTypes = 
    {
        "Cobalt", 
        "Gold",
        "Ice", 
        "Iron", 
        "Magnesium", 
        "Nickel",
        "Platinum",
        "Scrap",
        "Silicon",
        "Silver",
        "Stone",
        "Uranium"
    };

    int[] arraySums = new int[arrayTypes.Length];

    foreach (string name in arrayContainers) {
        
        IMyEntity container;
        container = GridTerminalSystem.GetBlockWithName(name) as IMyEntity;
        
        IMyInventory inventory;
        inventory = container.GetInventory();
        
        for (int i = 0; i < arrayTypes.Length; i++) {
            MyFixedPoint fixedPoint;
            MyItemType ItemType = new MyItemType("MyObjectBuilder_Ore", arrayTypes[i]);
            fixedPoint = inventory.GetItemAmount(ItemType);
            int intValue;
            intValue = fixedPoint.ToIntSafe();
            arraySums[i] += intValue;
        }
        
    }

    IMyTextSurface textSurface;
    textSurface = GridTerminalSystem.GetBlockWithName("Text Panel 2") as IMyTextSurface;
    textSurface.FontSize = 0.7F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";

    string output = "Ores in Cargo Containers:\n\n";

    for (int i = 0; i < arrayTypes.Length; i++) {
        output += arrayTypes[i] + " = " + arraySums[i].ToString() + " kg\n";
    }

    textSurface.WriteText(output, false);
}