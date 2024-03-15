using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameMaster : MonoBehaviour
{
    //Save file to write all data to
    GameData saveData = new GameData();

    //hold reference to current players
    [HideInInspector] public PlayerData currentPlayer1;
    [HideInInspector] public PlayerData currentPlayer2;

    //hold a temporary list of data for displaying highscores
    public List<PlayerData> tempPlayers = new List<PlayerData>(10);

    public bool debugButtons;
    //edit current players data like score and name
    private void Start()
    {
        saveData = new GameData();
    }

    //add our current players to the list
    //sort the list from hightest to lowest scores

    //convert the list to simple data arrays

    //create a temp list of all players, filled in with data from saveData
    public void CreateTempList()
    {
        //generate a new empty list of players
        tempPlayers = new List<PlayerData>();

        //get all the players from saveData and put them in the list.
        for (int i = 0; i < saveData.playerNames.Length; i++)
        {
            //create a player
            PlayerData newPlayer = new PlayerData();
            //input the data from the saveData to the new player
            newPlayer.playerName = saveData.playerNames[i];
            newPlayer.kills = saveData.kills[i];
            newPlayer.deaths = saveData.deaths[i];

            if (newPlayer.deaths == 0) newPlayer.kd = newPlayer.kills;
            else newPlayer.kd = newPlayer.kills / newPlayer.deaths;

            //add the new player to the list
            tempPlayers.Add(newPlayer);
        }
        
        //SortTempList(tempPlayers);

    }
    //sort the list from hightest to lowest scores
    public List<PlayerData> SortTempList(List<PlayerData> unSortedPlayers, bool addCurrentPlayers = false)
    {
        if (addCurrentPlayers)
        {
            //add the current players
            if (tempPlayers.Find(p => p.playerName == currentPlayer1.playerName) == null)//check if the temp list already contains a player with this name
            {
                tempPlayers.Add(currentPlayer1);//if the temp list did not return a PlayerData with the same name, then add this as a new entry;
            }
            if (tempPlayers.Find(p => p.playerName == currentPlayer2.playerName) == null)//check if the temp list already contains a player with this name
            {
                tempPlayers.Add(currentPlayer2);//if the temp list did not return a PlayerData with the same name, then add this as a new entry;
            }
        }
        //sort the list
        List<PlayerData> sortedPlayers = unSortedPlayers.OrderByDescending(p => p.kd).ToList();
        return sortedPlayers;
    }
    //save the arrays to saveData
    public void SendHighScoresToSaveData(List<PlayerData> players)
    {
        for (int i = 0; i < 10; i++)
        {
            //if (saveData[i] == null) Debug.Log("player not found");
            saveData.playerNames[i] = players[i].playerName;
            saveData.kills[i] = players[i].kills;
            saveData.deaths[i] = players[i].deaths;
        }
    }
    //save the game
    public void SaveGame()
    {
        if (saveData == null) Debug.Log("No Game data found");
        if (tempPlayers == null) Debug.Log("No list found");
        SendHighScoresToSaveData(tempPlayers);
        //input the currentPlayers to save Data
        saveData.lastPlayerNames[0] = currentPlayer1.playerName;
        saveData.lastKills[0] = currentPlayer1.kills;
        saveData.deaths[0] = currentPlayer1.deaths;

        saveData.lastPlayerNames[1] = currentPlayer2.playerName;
        saveData.lastKills[1] = currentPlayer2.kills;
        saveData.deaths[1] = currentPlayer2.deaths;
        SaveSystem.instance.SaveGame(saveData);
    }

    public void LoadGame()
    {
        //get a GameData file from the saved game files
        saveData = SaveSystem.instance.LoadGame();

        //input the new saveData from the loaded Game to the currentPlayer datas
        currentPlayer1.playerName = saveData.lastPlayerNames[0];
        currentPlayer1.kills = saveData.lastKills[0];
        currentPlayer1.deaths = saveData.deaths[0];

        currentPlayer2.playerName = saveData.lastPlayerNames[1];
        currentPlayer2.kills = saveData.lastKills[1];
        currentPlayer2.deaths = saveData.deaths[1];

        CreateTempList();
    }

    private void Update()
    {
        if (!debugButtons) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if(tempPlayers != null)
            {
               tempPlayers = SortTempList(tempPlayers, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }

    }

    //No Longer in Use
    /*
     * if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveData.AddScore(1);
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveData.AddScore(-1);
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.instance.SaveGame(saveData);
            Debug.Log("Saved Game");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveData = SaveSystem.instance.LoadGame();
            Debug.Log("Loaded data");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            saveData.ResetData();
            PrintScore();
        }
        public void PrintScore()
    {
        Debug.Log("The current score is " + saveData.score);
    }
     */
}
