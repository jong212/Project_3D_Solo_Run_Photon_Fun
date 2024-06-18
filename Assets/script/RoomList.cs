using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab; // ���� ��Ÿ���� ������
    List<RoomInfo> myList = new List<RoomInfo>(); // ���� �����ϴ� �� ��� ����Ʈ

    void Awake()
    {
        // �κ� �����Ͽ� �� ��� ������Ʈ�� ����
        // PhotonNetwork.JoinLobby();
    }

    // Photon PUN �ݹ� �޼��带 ����Ͽ� �� ��� ������Ʈ ó��
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

    // ���� �����ӿ��� ���� ��ġ�� �α��ϱ� ���� �ڷ�ƾ
    private IEnumerator LogPositionNextFrame(GameObject room)
    {
        yield return null; // ���� �����ӱ��� ���
        // ���⼭�� �ַ� ���� ��ġ�� ����ϰų� �ٸ� ó���� �߰��� �� �ֽ��ϴ�.
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        // �κ� �������� ��, �� ����� ������Ʈ�ϵ��� ����
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left Lobby");
        // �κ� ������ ���� ó��
    }
}
