using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class JoinRoomUI : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinField, createField;
    public TMP_Text debugText;
    public byte maxNumberPerRoom;

    public void AttemptToJoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }
    public void AttemptToCreateRoom()
    {
        PhotonNetwork.CreateRoom(createField.text, new RoomOptions { MaxPlayers = maxNumberPerRoom });
    }
}
