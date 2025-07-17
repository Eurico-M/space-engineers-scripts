public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
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

public string commaSeparators(int number) {
    
    StringBuilder output = new StringBuilder();
    int thousands = number/1000;
    int hundreds = number%1000;
    if (thousands > 0) {
        int millions = thousands/1000;
        int hundredThousands = thousands%1000;
        if (millions > 0) {
            output.Append(millions);
            output.Append(",");
            output.Append(hundredThousands.ToString("D3"));
            output.Append(",");
            output.Append(hundreds.ToString("D3"));
        } else {
            output.Append(thousands);
            output.Append(",");
            output.Append(hundreds.ToString("D3"));
        }
    } else {
        output.Append(hundreds);
    }
    
    return output.ToString();
}

public void Main(string argument, UpdateType updateSource)
{   
    string displayName = "Text Panel 2";
    IMyTextSurface textSurface;
    textSurface = GridTerminalSystem.GetBlockWithName(displayName) as IMyTextSurface;
    textSurface.FontSize = 0.8F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";
    
    List<IMyTerminalBlock> blocksInGrid = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType(blocksInGrid, block => block.IsSameConstructAs(Me));
    
    string itemType = "MyObjectBuilder_Ingot";
    
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
    
    string[] arrayNames = {
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
    
    foreach (IMyTerminalBlock blockName in blocksInGrid) {
        
        int numberInventories = blockName.InventoryCount;

        for (int i = 0; i < numberInventories; i++) {            
            IMyInventory inventory = blockName.GetInventory(i);
            
            for (int j = 0; j < arrayTypes.Length; j++) {

                int itemCount = sumItems(arrayTypes[j], itemType, inventory); 
                arraySums[j] += itemCount;
            }
        }
    }

    StringBuilder outputSB = new StringBuilder();
    outputSB.Append("Ingots in all Base Inventories:\n\n");

    for (int i = 0; i < arrayTypes.Length; i++) {
        
        StringBuilder outLine = new StringBuilder();
        int number = arraySums[i];
        outLine.Append(arrayNames[i]);
        int paddingStart = outLine.Length;
        outLine.Append(commaSeparators(number));
        outLine.Append(" kg\n");
        int paddingCount = 32 - outLine.Length;
        
        outLine.Insert(paddingStart, ' ');
        
        for (int j = 0; j < paddingCount - 1; j++) {
            outLine.Insert(paddingStart, '.');
        }

        outputSB.Append(outLine);
    }

    textSurface.WriteText(outputSB.ToString(), false);
}