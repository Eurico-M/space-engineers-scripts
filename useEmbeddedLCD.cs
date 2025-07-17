public Program()
{

}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{

    var lcdButtonPanel = GridTerminalSystem.GetBlockWithName("Button Outside Server Room") as IMyTextSurfaceProvider;
    IMyTextSurface textSurface = lcdButtonPanel.GetSurface(0);
    textSurface.ClearImagesFromSelection();
    textSurface.WriteText("\n\nPress for\nTech Support.", false);
}