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
    string[] displayNames = {
        "Kourkar Helm",
        "Control Seat Port Fore Guns",
        "Control Seat Port Aft Guns",
        "Control Seat Starboard Fore Guns",
        "Control Seat Starboard Aft Guns"
    };
    
    List<IMyTerminalBlock> blocksInGrid = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType(blocksInGrid, block => block.IsSameConstructAs(Me));
    
    string itemType = "MyObjectBuilder_AmmoMagazine";
    
    string[] arrayTypes = {
        "NATO_25x184mm",
        "MediumCalibreAmmo",
        "LargeCalibreAmmo"
    };
    
    string[] arrayNames = {
        "Gatling Ammo",
        "Cannon Ammo",
        "Artillery Ammo"
    };
    
    int[] maxAmmo = {
        600,           //multiples of 30 please
        200,
        200
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
    outputSB.Append("\n");

    for (int i = 0; i < arrayTypes.Length; i++) {
        
        StringBuilder outLine = new StringBuilder();
        int number = arraySums[i];
        outLine.Append(arrayNames[i]);
        int paddingStart = outLine.Length;
        outLine.Append(commaSeparators(number));
        outLine.Append("\n");
        int paddingCount = 32 - outLine.Length;
        
        outLine.Insert(paddingStart, ' ');
        
        for (int j = 0; j < paddingCount - 1; j++) {
            outLine.Insert(paddingStart, ' ');
        }

        outputSB.Append(outLine);
        
        StringBuilder graphic = new StringBuilder();
        int divisor = maxAmmo[i] / 30;
        int numberBars = number / divisor;
        int numberDots = 30 - numberBars;
        graphic.Append("[");
        for (int j = 0; j < numberBars; j++) {
            graphic.Append("|");
        }
        for (int j = 0; j < numberDots; j++) {
            graphic.Append(".");
        }
        graphic.Append("]\n\n");
        
        outputSB.Append(graphic);
    }
    
    for (int i = 0; i < displayNames.Length; i++) {
        
        IMyTextSurfaceProvider lcdButtonPanel = GridTerminalSystem.GetBlockWithName(displayNames[i]) as IMyTextSurfaceProvider;
        IMyTextSurface textSurface = lcdButtonPanel.GetSurface(0);
        textSurface.ContentType = ContentType.TEXT_AND_IMAGE;
        textSurface.ClearImagesFromSelection();
        textSurface.FontSize = 0.8F;
        textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
        textSurface.Font = "Monospace";
        textSurface.WriteText(outputSB.ToString(), false);
    }
}