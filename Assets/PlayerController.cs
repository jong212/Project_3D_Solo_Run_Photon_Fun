using Photon.Pun;
using UnityEngine;
using static NetworkManager;
public class PlayerController : MonoBehaviourPun
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // �÷��̾ R Ű�� ���� �غ� ���¸� �����Ѵٰ� ����
        {
            NM.SetPlayerReady(PhotonNetwork.LocalPlayer.ActorNumber, true);
        }
    }
}