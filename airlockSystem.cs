public Program()
{
}

public void Save()
{
}
public void outputText(string text) {
    IMyTextSurface textSurface = GridTerminalSystem.GetBlockWithName("TextPanelAirlock") as IMyTextSurface;
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

public void close(StringBuilder[] arrayDoors, string procedure, string airlockLetter) {
    foreach (StringBuilder name in arrayDoors) {
        IMyDoor door = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyDoor;
        door.Enabled = true;
        door.CloseDoor();
    }
    outputText("\n\nClosing Doors...");
    StringBuilder text = new StringBuilder();
    text.Append(procedure);
    text.Append(airlockLetter);
    text.Append("2");
    textToScreen(text.ToString());
    setTimer(3.0F);
    startTimerBlock();
}

public void depressurize(StringBuilder[] arrayVents, StringBuilder[] arrayDoors1 , StringBuilder[] arrayDoors2, string procedure, string airlockLetter) {
    foreach (StringBuilder name in arrayVents) {
        IMyAirVent vent = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyAirVent;
        vent.Depressurize = true;
    }
    outputText("\n\nDepressurizing...");
    disableDoors(arrayDoors1);
    disableDoors(arrayDoors2);
    StringBuilder text = new StringBuilder();
    text.Append(procedure);
    text.Append(airlockLetter);
    text.Append("3");
    textToScreen(text.ToString());
    setTimer(4.0F);
    startTimerBlock();
}

public void pressurize(StringBuilder[] arrayVents, StringBuilder[] arrayDoors1 , StringBuilder[] arrayDoors2, string procedure, string airlockLetter) {
    foreach (StringBuilder name in arrayVents) {
        IMyAirVent vent = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyAirVent;
        vent.Depressurize = false;
    }
    outputText("\n\nPressurizing...");
    disableDoors(arrayDoors1);
    disableDoors(arrayDoors2);
    StringBuilder text = new StringBuilder();
    text.Append(procedure);
    text.Append(airlockLetter);
    text.Append("3");
    textToScreen(text.ToString());
    setTimer(4.0F);
    startTimerBlock();
}

public void open(StringBuilder[] arrayDoors, string procedure, string airlockLetter) {
    foreach (StringBuilder name in arrayDoors) {
        IMyDoor door = GridTerminalSystem.GetBlockWithName(name.ToString()) as IMyDoor;
        door.Enabled = true;
        door.OpenDoor();
    }
    outputText("\n\nOpening Doors...");
    textToScreen("T" + airlockLetter + "0");
    setTimer(3.0F);
    startTimerBlock();
}

public void endCycle(StringBuilder[] arrayDoors1, StringBuilder[] arrayDoors2) {
    outputText("\n<-- Enter     Exit -->\n\nUse the buttons!");
    disableDoors(arrayDoors1);
    disableDoors(arrayDoors2);
}

public void disableDoors(StringBuilder[] doorNames) {
    foreach (StringBuilder name in doorNames) {
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
    
public void Main(string argument, UpdateType updateSource) {
    
    if (argument.Equals("X")) {
        argument = textFromScreen();
    }
    
    string procedure = argument.Substring(0,1);
    string airlockLetter = argument.Substring(1,1);
    string phase = argument.Substring(2,1);
    
    StringBuilder[] airlockDoorsOutside = new StringBuilder[2];
    StringBuilder[] airlockDoorsInside = new StringBuilder[2];
    StringBuilder[] airlockVents = new StringBuilder[2];
    
    StringBuilder outsideDoor1 = new StringBuilder("AirlockDoorOutside1");
    airlockDoorsOutside[0] = outsideDoor1;
    StringBuilder outsideDoor2 = new StringBuilder("AirlockDoorOutside2");
    airlockDoorsOutside[1] = outsideDoor2;
    StringBuilder insideDoor1 = new StringBuilder("AirlockDoorInside1");
    airlockDoorsInside[0] = insideDoor1;
    StringBuilder insideDoor2 = new StringBuilder("AirlockDoorInside2");
    airlockDoorsInside[1] = insideDoor2;
    StringBuilder airlockVent1 = new StringBuilder("AirlockVent1");
    airlockVents[0] = airlockVent1;
    StringBuilder airlockVent2 = new StringBuilder("AirlockVent2");
    airlockVents[1] = airlockVent2;
    
    foreach (StringBuilder name in airlockDoorsOutside) {
        name.Append(airlockLetter);
    }
    
    foreach (StringBuilder name in airlockDoorsInside) {
        name.Append(airlockLetter);
    }
    
    foreach (StringBuilder name in airlockVents) {
        name.Append(airlockLetter);
    }
    
    if (procedure.Equals("O")) {
        if (phase.Equals("1")) {
            close(airlockDoorsInside, procedure, airlockLetter);
        } else if (phase.Equals("2")) {
            depressurize(airlockVents, airlockDoorsOutside, airlockDoorsInside, procedure, airlockLetter);
        } else {
            open(airlockDoorsOutside, procedure, airlockLetter);
        }
    } else if (procedure.Equals("I")) {
        if (phase.Equals("1")) {
            close(airlockDoorsOutside, procedure, airlockLetter);
        } else if (phase.Equals("2")) {
            pressurize(airlockVents, airlockDoorsOutside, airlockDoorsInside, procedure, airlockLetter);
        } else {
            open(airlockDoorsInside, procedure, airlockLetter);
        }
    } else {
        endCycle(airlockDoorsInside, airlockDoorsOutside);
    }
}