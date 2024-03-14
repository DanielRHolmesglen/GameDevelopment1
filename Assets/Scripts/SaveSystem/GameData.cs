using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //you can only save simple data
    public int score = 0;
    public string player1Name;

    public void AddScore(int points)
    {
        score += points;
    }
    public void ResetData()
    {
        score = 0;
    }
}
