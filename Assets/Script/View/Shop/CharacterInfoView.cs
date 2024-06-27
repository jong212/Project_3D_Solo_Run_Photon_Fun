using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoView : MonoBehaviour
{
    [SerializeField] Text Text_Name;
    [SerializeField] Text Text_Description;
    [SerializeField] GameObject Transform_SlotRoot;
    [SerializeField] GameObject Prefab_SkillSlot;

    private List<Shop> _allCharacters;
    public void Start()
    {
        //SetCharacterInfo();
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
            var gObj = Instantiate(Prefab_SkillSlot, Transform_SlotRoot.transform);
            var skillSlot = gObj.GetComponent<ShopSloatView>();
            if (skillSlot == null)
                continue;

            skillSlot.SetUI(character);
        }

    }
}
