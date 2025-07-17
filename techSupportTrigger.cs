public Program()
{

}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{

    IMyTextSurfaceProvider lcdButtonPanel;
    lcdButtonPanel = GridTerminalSystem.GetBlockWithName("Button Outside Server Room") as IMyTextSurfaceProvider;
    IMyTextSurface textSurface = lcdButtonPanel.GetSurface(0);
    textSurface.ClearImagesFromSelection();
    textSurface.AddImageToSelection("Danger", true);
    textSurface.WriteText("RETARD ALERT!", false);
    
    string[] arrayLights = {
        "Inset Light Server Corridor 0",
        "Inset Light Server Corridor 1",
        "Inset Light Server Corridor 2",
        "Inset Light Server Corridor 3"
    };
    
    foreach (string blockName in arrayLights) {
        IMyInteriorLight light;
        light = GridTerminalSystem.GetBlockWithName(blockName) as IMyInteriorLight;
        light.Color = VRageMath.Color.Tomato;
    }
}