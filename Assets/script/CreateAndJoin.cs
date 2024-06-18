using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;


    public void CreateRoom()
    {
        //PhotonNetwork.JoinRandomOrCreateRoom(null, 4);
        PhotonNetwork.CreateRoom(input_Create.text,new RoomOptions { MaxPlayers = 2});
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }
    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
        print("�濡 ������ �÷��̾� �ο� �� " + PhotonNetwork.CountOfPlayersInRooms);
    }
    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        { 

            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            print("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }
}
