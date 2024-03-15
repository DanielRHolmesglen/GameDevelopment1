using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds the savable information for individual players including name and score
/// students should customize this to suit the way there game scores
/// </summary>

[System.Serializable]
public class PlayerData 
{
    public string playerName;
    public int kills;
    public int deaths;
    public float kd; //kills divided by deaths. used for the actual sorting of scores
}
