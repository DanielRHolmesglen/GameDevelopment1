using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manage game state, scores, and respawns during gameplay. Only one GamePlayManager should be in the scene
/// reference this script to access gameplay objects like UI
/// </summary>
public class GamePlayManager : MonoBehaviour
{
    #region Variables
    //store gameplay states
    public enum State { Intro, Gameplay, Pause, Ending }
    [Header("Game Settings")]
    public State gameState = State.Intro;
    //store score data
    public int player1Score, player2Score;

    public int maxScore;
    public float gameDuration;

    //store respawns positions
    public Transform[] respawnPositions;

    [Header("Player Settings")]
    //store player object references
    public GameObject player1;
    public GameObject player2;
    //store player data
    public string player1Name, player2Name;

    [Header("UI Settings")]
    //store UI references
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text timerText;
    public TMP_Text messageText;

    
    [Header("Other Components")]
    //store a timer
    public Timer gameTimer;

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Score update function - increase one players score, check if game is over, update UI
    public void UpdateScore(int playerNumber)
    {
        //if playerNumber is 1, increase player1 score, otherwise increase player 2
        //check if either players score is more than the max score. If so, call End game function
        //update the score UI
    }

    //respawns - find player to respawn, deactivate controls, decrease lives*, 
    //disable player, move to spawn point, reset data, reactivate player, check for end of game if relevant

    //Display the timer

    //run intro sequence

    //begin gameplayer sequence - start the timer, activate the players, update ui

    //End game - freeze players, tally scores, display results or move to next scene
}
