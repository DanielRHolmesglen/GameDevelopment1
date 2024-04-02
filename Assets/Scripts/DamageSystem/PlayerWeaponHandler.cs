using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerNumber;

    [Header("Weapons")]
    public bool canSwap = false;
    public List<IShootable> weapons = new List<IShootable>();
    public int currentWeapon = 0;

    private KeyCode shootKey; //what button will trigger the shooting. This is set depending on the player number
    private KeyCode swap; //what button will swap the weapon. this is set depending on the player number again.

    // Start is called before the first frame update
    void Start()
    {
        SetUpInputs();
        currentWeapon = 0;
        ActivateSelectedWeapon();
    }
    private void Update()
    {
        if (Input.GetKeyDown(shootKey)) Fire();
        if (Input.GetKeyDown(swap)) Swap();
    }
    private void Swap()
    {
        currentWeapon = currentWeapon + 1 % weapons.Count;
        ActivateSelectedWeapon();
    }
    private void Fire()
    {
        weapons[currentWeapon].Shoot();
    }
    private void ActivateSelectedWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == currentWeapon)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else weapons[i].gameObject.SetActive(false);
        }
    }
    private void SetUpInputs()
    {
        switch (playerNumber)
        {
            case 1:
                shootKey = KeyCode.V;
                swap = KeyCode.C;
                break;
            case 2:
                shootKey = KeyCode.V;
                swap = KeyCode.C;
                break;
            case 0:
                Debug.Log("No player number assigned. Controls could not be selected");
                break;
        }
           
    }
    
}
