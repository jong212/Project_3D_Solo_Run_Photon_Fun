using Photon.Pun;
using UnityEngine;

namespace ithappy
{
    public class Rnd_Animation : MonoBehaviourPun, IPunObservable
    {
        Animator anim;
        float offsetAnim;
        Quaternion syncedRotation;

        [SerializeField] string titleAnim;

        void Awake()
        {
            // �ִϸ����͸� Awake���� �ʱ�ȭ
            anim = GetComponent<Animator>();
        }

        void Start()
        {
            if (titleAnim != "Swing_X") return;

            if (PhotonNetwork.IsMasterClient)
            {
                // ������ Ŭ���̾�Ʈ���� ���� ������ ����
                offsetAnim = Random.Range(0f, 1f);
                Debug.Log($"MasterClient: Playing animation {titleAnim} with offset {offsetAnim}");
                anim.Play(titleAnim, 0, offsetAnim);  // ������ Ŭ���̾�Ʈ���� �ִϸ��̼� ����
            }
        }

        void Update()
        {
            if (titleAnim != "Swing_X") return;
            if (!photonView.IsMine)
            {
                // ���� ȸ�� ����
                transform.rotation = Quaternion.RotateTowards(transform.rotation, syncedRotation, Time.deltaTime * 100);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // ������ Ŭ���̾�Ʈ�� ���� ȸ�� ���� ����
                stream.SendNext(transform.rotation);
            }
            else
            {
                // Ŭ���̾�Ʈ�� ȸ�� ���� ����
                syncedRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
