using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    GameData saveData = new GameData();

    //hold reference to current players

    //edit current players data like score and name

    //create a temp list of all players, filled in with data from saveData
    //add our current players to the list
    //sort the list from hightest to lowest scores

    //convert the list to simple data arrays
    //save the arrays to saveData
    //save the game

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
    }
    public void PrintScore()
    {
        Debug.Log("The current score is " + saveData.score);
    }
}
