using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public static ConnectToServer instance;
    [SerializeField] public TMP_InputField input_Create;
    [SerializeField] public TMP_InputField input_Join;  
    public GameObject RoomPrefab; // ���� ��Ÿ���� ������ 
    List<RoomInfo> myList = new List<RoomInfo>(); // ���� �����ϴ� �� ��� ����Ʈs
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ConnectToPhoton();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // myList�� �ִ� �� �̸� ����Ʈ ����
        List<string> currentRoomNames = new List<string>();
        foreach (RoomInfo room in myList)
        {
            currentRoomNames.Add(room.Name);
        }

        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                // ���� ���ŵ��� ���� ���, �߰� �Ǵ� ������Ʈ ó��
                if (!myList.Contains(roomList[i]))
                {
                    // ���ο� ���̹Ƿ� myList�� �߰��ϰ� RoomPrefab�� �ν��Ͻ�ȭ�Ͽ� UI�� ǥ��
                    myList.Add(roomList[i]);
                    GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                    Room.transform.localPosition = Vector3.zero;
                    Room.GetComponent<Room>().Name.text = roomList[i].Name; // Room ��ũ��Ʈ�� RoomPrefab�� �����Ǿ� �ִٰ� ����
                    StartCoroutine(LogPositionNextFrame(Room)); // ���� �����ӿ��� ��ġ�� �α��ϱ� ���� �ڷ�ƾ ����
                }
                else
                {
                    // myList�� �̹� �ִ� ���̹Ƿ� ������Ʈ ó��
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
                }
            }
            else
            {
                // ���� ��Ͽ��� ���ŵ� ���, myList������ ����
                if (myList.Contains(roomList[i]))
                {
                    myList.Remove(roomList[i]);

                    // �ش� GameObject�� ã�Ƽ� �ı�
                    Transform contentTransform = GameObject.Find("Content").transform;
                    foreach (Transform child in contentTransform)
                    {
                        Room roomComponent = child.GetComponent<Room>();
                        if (roomComponent != null && roomComponent.Name.text == roomList[i].Name)
                        {
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
    public void CreateRoom()
    {
        Debug.Log(input_Create.text);
        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }

    public void JoinRoomInList(string RoomName)
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinRoom(RoomName);
            }
            else
            {
                PhotonNetwork.JoinLobby();
            }
        }
        else
        {
            Debug.LogError("Cannot join room: Not connected to Master Server");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
            PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
        Debug.Log("�濡 ������ �÷��̾� �ο� �� " + PhotonNetwork.CountOfPlayersInRooms);
    }

    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            Debug.Log(playerStr);
        }
        else
        {
            Debug.Log("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            Debug.Log("�� ���� : " + PhotonNetwork.CountOfRooms);
            Debug.Log("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            Debug.Log("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }
    private IEnumerator LogPositionNextFrame(GameObject room)
    {
        yield return null; // ���� �����ӱ��� ���
        // ���⼭�� �ַ� ���� ��ġ�� ����ϰų� �ٸ� ó���� �߰��� �� �ֽ��ϴ�.
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon Server: " + cause.ToString());
        // ������ ������ �� �߰� �۾��� ���⿡ �ۼ�
    }

    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
