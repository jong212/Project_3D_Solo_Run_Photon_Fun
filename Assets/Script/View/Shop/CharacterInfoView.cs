using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� Ui ���� �� Container�� �� ��ũ��Ʈ�� �����
public class CharacterInfoView : MonoBehaviour
{
    [SerializeField] Text Text_Name;
    [SerializeField] Text Text_Description;
    [SerializeField] GameObject Transform_SlotRoot;
    [SerializeField] GameObject Prefab_SkillSlot;
    public NetworkManager networkManager;

    private List<Shop> _allCharacters;
    public void Awake()
    {
        var networkManagerObject = GameObject.FindWithTag("NetManager");
        if (networkManagerObject != null)
        {
            networkManager = networkManagerObject.GetComponent<NetworkManager>();
        }

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager not found in the scene.");
            return;
        }
    }
    private void Start()
    {
        LoadAllCharacterData();

    }
    private void OnEnable()
    {
        // Remove all child objects from Transform_SlotRoot
        foreach (Transform child in Transform_SlotRoot.transform)
        {
            Destroy(child.gameObject);
        }

        // Load character data
        LoadAllCharacterData();
    }

    private void LoadAllCharacterData()
    {
        _allCharacters = LobbyDataManager.Inst.GetAllCharacterData();
        if (_allCharacters == null || _allCharacters.Count == 0)
        {
            return;
        }
        foreach (var character in _allCharacters)
        {
            // ���⼭ �� ĳ���Ϳ� ���� �ʿ��� �ʱ�ȭ �۾��� ����
            //Debug.Log($"Initializing character: {character.Name}");

            // �� ĳ������ ��ų UI�� ����
            bool temp_chk = false;
            var gObj = Instantiate(Prefab_SkillSlot, Transform_SlotRoot.transform);
            var skillSlot = gObj.GetComponent<ShopSloatView>();
            if (skillSlot == null)
                continue;
            if (networkManager != null)
            {
                foreach (var cName in networkManager.OwnedCharacters)
                {
                    if (character.Name == cName)
                    {
                        temp_chk = true;
                        break;
                    }
                }

            }
            skillSlot.SetUI(character, temp_chk);
        }

    }
}
