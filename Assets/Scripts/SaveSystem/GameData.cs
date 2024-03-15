using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The savable data for your whole game. Important game data is stored in an instance of this script
/// Save the game by sending an instance of this script to the SaveSystem and load a game by loading and instance from the SaveStystem
/// </summary>
[System.Serializable]
public class GameData
{
    //List of all players that have had there score saved
    public string[] playerNames = new string[10];
    //A collection of the scores associated with each playerName
    public int[] kills = new int[10];
    public int[] deaths = new int[10];
    

    //Game information
    public float maxRoundTime;
    public int maxKills;
    //List of all last that have had there score saved
    public string[] lastPlayerNames = new string [2];
    //A collection of the last scores associated with each playerName
    public int[] lastKills = new int[10];
    public int[] lastDeaths = new int[10];
    

    //NOTE: AddScore and ResetScore have been removed.
}
