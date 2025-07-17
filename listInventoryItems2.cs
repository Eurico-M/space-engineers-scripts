public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{   
    string blockName = "insert";
    
    string[] arrayContainers = {
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

    string[] arrayTypes = {
        "Medical",
        "MetalGrid",
        "Motor", 
        "PowerCell",
        "RadioCommunication", 
        "Reactor", 
        "SmallTube", 
        "SolarCell",
        "SteelPlate",
        "Superconductor",
        "Thrust"
    };
    
    string[] arrayNames = {
        "Medical Comp.",
        "Metal Grids",
        "Motors",
        "Power Cells",
        "Radio-comm Comp.",
        "Reactor Comp.",
        "Small Steel Tubes",
        "Solar Cells",
        "Steel Plates",
        "Superconductors",
        "Thruster Comp."
    };

    int[] arraySums = new int[arrayTypes.Length];

    foreach (string name in arrayContainers) {
        
        IMyEntity container;
        container = GridTerminalSystem.GetBlockWithName(name) as IMyEntity;
        
        IMyInventory inventory;
        inventory = container.GetInventory();
        
        for (int i = 0; i < arrayTypes.Length; i++) {
            MyFixedPoint fixedPoint;
            MyItemType ItemType = new MyItemType("MyObjectBuilder_Component", arrayTypes[i]);
            fixedPoint = inventory.GetItemAmount(ItemType);
            int intValue;
            intValue = fixedPoint.ToIntSafe();
            arraySums[i] += intValue;
        }
    }

    IMyTextSurface textSurface;
    textSurface = GridTerminalSystem.GetBlockWithName(blockName) as IMyTextSurface;
    textSurface.FontSize = 0.7F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";

    string output = "Components in Cargo Containers:\n\n";

    for (int i = 0; i < arrayTypes.Length; i++) {
        output += arrayNames[i] + " = " + arraySums[i].ToString() + " units\n";
    }

    textSurface.WriteText(output, false);
}