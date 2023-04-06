using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public string PlayerName;

    public ClockTime Time;

    public BehaviorSet[] NPCBehaviorSettings;

    public PositionSettingsConversion PositionSettings = new();

    public bool[] LightSettings;

    public bool[] PaperSettings;
    
    public bool[] PenSettings;

    public bool[] KeySettings;
    
    public bool[] BookSettings;

    public bool[] MiscSettings;

    public int PensHeld;

    public int PaperHeld;

    public int BooksHeld;

    public Letter[] LettersHeld;

    public LogItem[] Logs;

    public int LogsCounter;

    public Dictionary<string, Person> PeopleKnown;
}
