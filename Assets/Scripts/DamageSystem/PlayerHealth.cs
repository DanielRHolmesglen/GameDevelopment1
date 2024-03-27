using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    //variables for assigning who should score a point on the players death
    public int lastDamagedBy; //the number of the player that last damaged this character
    public int playerNum; //the number of this player;

    //variables for ressetting the lastDamagedBy setting;
    private float lastHitTime;
    private float damagedByReset = 3f;

    private Animator anim; //must reference the animator in order to play death animations

    private void Start()
    {
        lastDamagedBy = playerNum; //default this number to the players number. This means that if the player is killed by an environment object it will subtract points from itself;
        anim.SetBool("Dead", false);
    }
    public override void Die()
    {
        base.Die();
        anim.SetBool("Dead", true);

        //update scores
        if (lastDamagedBy == playerNum) GamePlayManager.instance.UpdateScore(playerNum, -1); //if the last player to damage this player was itself, then reduce its score by 1
        else GamePlayManager.instance.UpdateScore(lastDamagedBy, 1); //else, if the last player to damage this player was someone else, add to their score

        //make the character respawn, but on a delay so that the animation can play out
        StartCoroutine(RespawnCharacterOnDelay(playerNum));
    }
    IEnumerator RespawnCharacterOnDelay(int playerNumber)
    {
        yield return null;
    }
}
