using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class PlayerHealth : Health
{
    //variables for assigning who should score a point on the players death
    public int lastDamagedBy; //the number of the player that last damaged this character
    public int playerNum; //the number of this player;

    //variables for ressetting the lastDamagedBy setting;
    private float damagedByReset = 3f;
    public Slider slider;
    private Animator anim; //must reference the animator in order to play death animations
    PhotonView view;
    public bool isOnline;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        lastDamagedBy = playerNum; //default this number to the players number. This means that if the player is killed by an environment object it will subtract points from itself;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Dead", false);
    }
    public override void TakeDamage(float damageAmount)
    {
        if (isOnline && !view.IsMine) return;
        base.TakeDamage(damageAmount);
        if(slider)slider.value = health;
    }
    public override void Die()
    {
        if (isOnline && !view.IsMine) return;
        if (dead) return;
        base.Die();
        dead = true;
        anim.SetBool("Dead", true);
        //update scores
        if (lastDamagedBy == playerNum)
        {
            Debug.Log("Killed By self");
            if (isOnline) GamePlayManager.instance.view.RPC("UpdateScore", RpcTarget.All, playerNum, -1);
            else GamePlayManager.instance.UpdateScore(playerNum, -1); //if the last player to damage this player was itself, then reduce its score by 1
        }
        else
        {
            Debug.Log("Killed By Other");
            if (isOnline) GamePlayManager.instance.view.RPC("UpdateScore", RpcTarget.All, lastDamagedBy, 1);
            else GamePlayManager.instance.UpdateScore(lastDamagedBy, 1); //else, if the last player to damage this player was someone else, add to their score
        }
        //make the character respawn, but on a delay so that the animation can play out
        StartCoroutine(RespawnCharacterOnDelay(playerNum));
    }
    IEnumerator RespawnCharacterOnDelay(int playerNumber)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("attempt to spawn");
        if (isOnline) GamePlayManager.instance.view.RPC("SpawnPlayer", RpcTarget.All, playerNum);
        else GamePlayManager.instance.SpawnPlayer(playerNum);
        yield return null;
    }
    public IEnumerator ResetDamagedBy()
    {
        yield return new WaitForSeconds(damagedByReset);
        lastDamagedBy = playerNum;
    }
}
