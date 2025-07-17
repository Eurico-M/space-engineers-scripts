public Program()
{
}

public void Save()
{
}

public string textFromScreen() {
    IMyTextSurface screen = pBlockScreen();
    StringBuilder outputSB = new StringBuilder();
    screen.ReadText(outputSB, false);
    return outputSB.ToString();
}

public void textToScreen(string text) {
    IMyTextSurface screen = pBlockScreen();
    screen.ContentType = ContentType.TEXT_AND_IMAGE;
    screen.WriteText(text, false);
}

public IMyTextSurface pBlockScreen() {
    var pBlock = GridTerminalSystem.GetBlockWithName("[SALVOPB]") as IMyTextSurfaceProvider;
    IMyTextSurface pBlockScreen = pBlock.GetSurface(0);
    return pBlockScreen;
}

public int numberOfMissiles = 16; 

public void Main(string argument, UpdateType updateSource) {
    
    var timerBlock = GridTerminalSystem.GetBlockWithName("[SALVOTIMER]") as IMyTimerBlock;
    var fireBlock = GridTerminalSystem.GetBlockWithName("[LAMP] PB") as IMyProgrammableBlock;

    timerBlock.TriggerDelay = 2.0F;
    
    string myArgument = textFromScreen();
       
    int missiles = Int32.Parse(myArgument);
    
    if (missiles > 0) {
        fireBlock.TryRun("fire");
        timerBlock.StartCountdown();
        missiles--;
        string output = missiles.ToString();
        textToScreen(output);        
    } else {
        textToScreen("16");
    }
}