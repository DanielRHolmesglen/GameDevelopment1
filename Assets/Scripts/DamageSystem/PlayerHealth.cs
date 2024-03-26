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
    }

}
