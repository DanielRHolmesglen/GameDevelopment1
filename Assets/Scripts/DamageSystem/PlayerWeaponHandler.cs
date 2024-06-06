using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerNumber;

    [Header("Weapons")]
    public bool canSwap = false;
    public List<GameObject> weapons = new List<GameObject>();
    public int currentWeaponIndex = 0;
    private IShootable currentWeapon;

    private KeyCode shootKey; //what button will trigger the shooting. This is set depending on the player number
    private KeyCode swap; //what button will swap the weapon. this is set depending on the player number again.

    private PhotonView view;
    public bool isOnline = false;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        SetUpInputs();
        currentWeaponIndex = 0;
        ActivateSelectedWeapon();
    }
    private void Update()
    {
        if (isOnline && view.IsMine == false) return; //cancel the inputs if we are online but this is not our player

        if (Input.GetKeyDown(shootKey)) Fire();
        if (Input.GetKeyDown(swap)) Swap();
    }
    private void Swap()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        ActivateSelectedWeapon();
    }
    private void Fire()
    {
        if (!isOnline) //use the basic shoot if we are offline
        {
            currentWeapon.Shoot();
        }
        else //use the Remote Precedure call if we are online
        {
            view.RPC("OnlineFire", RpcTarget.All);
        }
    }
    [PunRPC]
    public void OnlineFire()
    {
        currentWeapon.Shoot();
    }
    private void ActivateSelectedWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == currentWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
                currentWeapon = weapons[i].GetComponent<IShootable>();
            }
            else weapons[i].gameObject.SetActive(false);
        }
    }
    private void SetUpInputs()
    {
        if (isOnline)
        {
            shootKey = KeyCode.V;
            swap = KeyCode.C;
        }
        else
        {
            switch (playerNumber)
            {
                case 1:
                    shootKey = KeyCode.V;
                    swap = KeyCode.C;
                    break;
                case 2:
                    shootKey = KeyCode.Comma;
                    swap = KeyCode.Period;
                    break;
                case 0:
                    Debug.Log("No player number assigned. Controls could not be selected");
                    break;
            }
        }
        
           
    }
    
}
