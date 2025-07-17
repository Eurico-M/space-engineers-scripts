public Program()
{
}

public void Save()
{
}

public void setButtons(bool state) {
    foreach (StringBuilder name in airlockButtons) {
        IMyFunctionalBlock button = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyFunctionalBlock;
        button.Enabled = state;
    }
}

public string getTextPanelName() {
    StringBuilder outputSB = new StringBuilder("AirlockTextPanel");
    outputSB.Append(airlockLetter);
    return outputSB.ToString();
}

public void outputText(string text) {
    string textPanelName = getTextPanelName();
    IMyTextSurface textSurface = GridTerminalSystem.GetBlockWithName(textPanelName) as IMyTextSurface;
    textSurface.ContentType = ContentType.TEXT_AND_IMAGE;
    textSurface.ClearImagesFromSelection();
    textSurface.FontSize = 1.0F;
    textSurface.Alignment = VRage.Game.GUI.TextPanel.TextAlignment.CENTER;
    textSurface.Font = "Monospace";
    textSurface.WriteText(text, false);
}

public IMyTextSurface pBlockScreen() {
    string name = "PB.airlockSystem";
    IMyTextSurfaceProvider pBlock = GridTerminalSystem.GetBlockWithName(name) as IMyTextSurfaceProvider;
    IMyTextSurface pBlockScreen = pBlock.GetSurface(0);
    return pBlockScreen;
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

public void close(List<StringBuilder> doors) {
    foreach (StringBuilder name in doors) {
        IMyDoor door = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyDoor;
        door.Enabled = true;
        door.CloseDoor();
    }
    setButtons(false);
    outputText("\n\nClosing Doors...");
    textToScreen(procedure + airlockLetter + "2");
    setTimer(3.0F);
    startTimerBlock();
}

public void depressurize() {
    foreach (StringBuilder name in airlockVents) {
        IMyAirVent vent = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyAirVent;
        vent.Depressurize = true;
    }
    outputText("\n\n\nDepressurizing...");
    disableDoors(airlockDoorsInside);
    disableDoors(airlockDoorsOutside);
    textToScreen(procedure + airlockLetter + "3");
    setTimer(4.0F);
    startTimerBlock();
}

public void pressurize() {
    foreach (StringBuilder name in airlockVents) {
        IMyAirVent vent = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyAirVent;
        vent.Depressurize = false;
    }
    outputText("\n\n\nPressurizing...");
    disableDoors(airlockDoorsInside);
    disableDoors(airlockDoorsOutside);
    textToScreen(procedure + airlockLetter + "3");
    setTimer(4.0F);
    startTimerBlock();
}

public void open(List<StringBuilder> doors) {
    foreach (StringBuilder name in doors) {
        IMyDoor door = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyDoor;
        door.Enabled = true;
        door.OpenDoor();
    }
    outputText("\n\n\n\nOpening Doors...");
    textToScreen("T" + airlockLetter + "0");
    setTimer(3.0F);
    startTimerBlock();
}

public void endCycle() {
    outputText("\n<-- Enter       Exit -->\n\n\nUse the buttons!");
    disableDoors(airlockDoorsOutside);
    disableDoors(airlockDoorsInside);
    setButtons(true);
}

public void disableDoors(List<StringBuilder> doors) {
    foreach (StringBuilder name in doors) {
        IMyDoor door = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyDoor;
        door.Enabled = false;
    }
}

public IMyTimerBlock timerBlock() {
    string name = "TB.airlockSystem";
    IMyTimerBlock tBlock = GridTerminalSystem.GetBlockWithName(name) as IMyTimerBlock;
    return tBlock;
}

public void startTimerBlock() {
    IMyTimerBlock timer = timerBlock();
    timer.StartCountdown();
}

public void setTimer(float time) {
    IMyTimerBlock timer = timerBlock();
    timer.TriggerDelay = time;
}

public static string airlockLetter = "";
public static string procedure = "";
public static string phase = "";

public static List<StringBuilder> airlockDoorsOutside = new List<StringBuilder>();
public static List<StringBuilder> airlockDoorsInside = new List<StringBuilder>();
public static List<StringBuilder> airlockVents = new List<StringBuilder>();
public static List<StringBuilder> airlockButtons = new List<StringBuilder>();

public void Main(string argument, UpdateType updateSource) {
    
    if (argument.Equals("X")) {
        argument = textFromScreen();
    }
    
    procedure = argument.Substring(0,1);
    airlockLetter = argument.Substring(1,1);
    phase = argument.Substring(2,1);
    
    int numberOutsideDoors = 0;
    int numberInsideDoors = 0;
    int numberAirVents = 0;
    int numberButtons = 0;
    
    switch (airlockLetter)
    {
        case "A":
            numberOutsideDoors = 1;
            numberInsideDoors = 1;
            numberAirVents = 2;
            numberButtons = 4;
            break;
    }
    
    for (int i = 1; i < numberOutsideDoors + 1; i++) {
        StringBuilder name = new StringBuilder("AirlockDoorOutside");
        name.Append(i);
        name.Append(airlockLetter);
        airlockDoorsOutside.Add(name);
    }
    
    for (int i = 1; i < numberInsideDoors + 1; i++) {
        StringBuilder name = new StringBuilder("AirlockDoorInside");
        name.Append(i);
        name.Append(airlockLetter);
        airlockDoorsInside.Add(name);
    }
    
    for (int i = 1; i < numberAirVents + 1; i++) {
        StringBuilder name = new StringBuilder("AirlockVent");
        name.Append(i);
        name.Append(airlockLetter);
        airlockVents.Add(name);
    }
    
    for (int i = 1; i < numberButtons + 1; i++) {
        StringBuilder name = new StringBuilder("AirlockButton");
        name.Append(i);
        name.Append(airlockLetter);
        airlockButtons.Add(name);
    }
        
    if (procedure.Equals("O")) {
        if (phase.Equals("1")) {
            close(airlockDoorsInside);
        } else if (phase.Equals("2")) {
            depressurize();
        } else {
            open(airlockDoorsOutside);
        }
    } else if (procedure.Equals("I")) {
        if (phase.Equals("1")) {
            close(airlockDoorsOutside);
        } else if (phase.Equals("2")) {
            pressurize();
        } else {
            open(airlockDoorsInside);
        }
    } else {
        endCycle();
    }
}