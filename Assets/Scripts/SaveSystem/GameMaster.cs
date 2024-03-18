using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameMaster : MonoBehaviour
{
    //Save file to write all data to
    public GameData saveData;

    #region singleton pattern
    public static GameMaster instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    //hold reference to current players
    [HideInInspector] public PlayerData currentPlayer1;
    [HideInInspector] public PlayerData currentPlayer2;

    //hold a temporary list of data for displaying highscores
    public List<PlayerData> tempPlayers = new List<PlayerData>(10);

    public bool debugButtons;
    public bool loadOnStart = true;
    //edit current players data like score and name
    private void Start()
    {

        //attempt to load data on game start
        if (loadOnStart)
        {
            LoadGame();
        }
        else
        {
            saveData = new GameData();
            CreateTempList();
        }
        
    }

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
            saveData.playerNames[i] = players[i].playerName;
            saveData.kills[i] = players[i].kills;
            saveData.deaths[i] = players[i].deaths;
        }
    }
    //save the game
    public void SaveGame()
    {
        SortTempList(tempPlayers, false);
        SendHighScoresToSaveData(tempPlayers);
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
        if (saveData == null)
        {
            saveData = new GameData();
            Debug.Log("no data was found, new file created");
        }
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomFillData();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearData();
        }

    }

    #region debugging functions
    void ClearData()
    {
        foreach (PlayerData player in tempPlayers)
        {
            player.playerName = "";
            player.kills = 0;
            player.deaths = 0;
            player.kd = 0;
        }
    }
    void RandomFillData()
    {
        //create possible letters to randomise from
        string glyphs = "abcdefghijklmnopqrstuvwxyz";

        foreach(PlayerData player in tempPlayers)
        {
            //generate a random name for the temp player
            int charAmount = Random.Range(3, 10);
            player.playerName = "";
            for (int i = 0; i < charAmount; i++)
            {
                player.playerName += glyphs[Random.Range(0, glyphs.Length)];
            }
            //generate random Kills score
            player.kills = Random.Range(0, 20);
            //generate random deaths
            player.deaths = Random.Range(0, 20);
            //calculate kd
            if (player.deaths == 0) player.kd = player.kills;
            else player.kd = player.kills / player.deaths;
        }

    }
    #endregion

    #region old code
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
    #endregion
}
