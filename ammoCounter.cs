public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{
    
    string[] arrayContainers = {
        "Artillery",
        "Artillery 2",
        "Assault Cannon Ventral Port Aft",
        "Assault Cannon Ventral Starboard Aft",
        "Assault Cannon Ventral Starboard Fore",
        "Basalt Aft Container",
        "Basalt Assault Cannon Dorsal Port Aft",
        "Basalt Assault Cannon Dorsal Port Fore",
        "Basalt Assault Cannon Dorsal Starboard Aft",
        "Basalt Assault Cannon Turret Dorsal Starboard Fore",
        "Basalt Bilge Container",
        "Basalt Gatling Port Aft",
        "Basalt Gatling Port Fore",
        "Basalt Gatling Starboard Aft",
        "Basalt Gatling Starboard Fore",
        "Basalt Port Container",
        "Bridge Helm 2",
        "Control Seat Port Aft Guns",
        "Control Seat Starboard Aft Guns",
        "Control Seat Starboard Fore Guns",
        "Inset Connector",
        "Inset Connector 2",
        "Interior Turret"
    };
    
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
    
    int[] arraySums = new int[arrayTypes.Length];
    
    foreach (string name in arrayContainers) {
        
        IMyEntity container;
        container = GridTerminalSystem.GetBlockWithName(name) as IMyEntity;
        
        IMyInventory inventory;
        inventory = container.GetInventory();
        
        for (int i = 0; i < arrayTypes.Length; i++) {
            MyFixedPoint fixedPoint;
            MyItemType ItemType = new MyItemType("MyObjectBuilder_AmmoMagazine", arrayTypes[i]);
            fixedPoint = inventory.GetItemAmount(ItemType);
            int intValue;
            intValue = fixedPoint.ToIntSafe();
            arraySums[i] += intValue;
        }
    }
    
    string blockName = "Control Seat Port Fore Guns";
    
    var lcdButtonPanel = GridTerminalSystem.GetBlockWithName(blockName) as IMyTextSurfaceProvider;
    IMyTextSurface textSurface = lcdButtonPanel.GetSurface(0);
    textSurface.ClearImagesFromSelection();
    textSurface.FontSize = 0.8F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";
    
    string output = "Ammo Counter\n\n";
    
    for (int i = 0; i < arrayTypes.Length; i++) {
        output += arrayNames[i] + " = " + arraySums[i].ToString() + " units\n";
    }
    textSurface.WriteText(output, false);
}