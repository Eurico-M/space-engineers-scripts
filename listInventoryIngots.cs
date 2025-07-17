public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{   
            
    string blockName = "Text Panel 2";
    
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
        "Cobalt", 
        "Gold", 
        "Stone", 
        "Iron", 
        "Magnesium", 
        "Nickel",
        "Platinum",
        "Silicon",
        "Silver",
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
            MyItemType ItemType = new MyItemType("MyObjectBuilder_Ingot", arrayTypes[i]);
            fixedPoint = inventory.GetItemAmount(ItemType);
            int intValue;
            intValue = fixedPoint.ToIntSafe();
            arraySums[i] += intValue;
        }
    }

    IMyTextSurface textSurface;
    textSurface = GridTerminalSystem.GetBlockWithName(blockName) as IMyTextSurface;
    textSurface.FontSize = 0.8F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";

    string output = "Ingots in Cargo Containers:\n\n";

    for (int i = 0; i < arrayTypes.Length; i++) {
        string outputNumber = "";
        int number = arraySums[i];
        int thousands = number/1000;
        int hundreds = number%1000;
        if (thousands > 0) {
            int millions = thousands/1000;
            int hundredThousands = thousands%1000;
            if (millions > 0) {
                outputNumber = millions.ToString() + "," + hundredThousands.ToString("D3") + "," + hundreds.ToString("D3");
            } else {
            outputNumber = thousands.ToString() + "," + hundreds.ToString("D3");
            }
        } else {
            outputNumber = hundreds.ToString();
        }
        output += arrayTypes[i] + " = " + outputNumber + " kg\n";
    }

    textSurface.WriteText(output, false);
}