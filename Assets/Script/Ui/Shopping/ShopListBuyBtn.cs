using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopListBuyBtn : MonoBehaviour
{
    private Button _btn;
    [SerializeField] private Text _PriceValue;
    [SerializeField] private Text _CharacterNameValue;
    void Start()
    {
        _btn = GetComponent<Button>();
        if(_btn!= null)
        {
            _btn.onClick.AddListener(CallParentFunction);
        }
    }
    void CallParentFunction()
    {
        // �θ� ������Ʈ ã��
        MainUI parent = GetComponentInParent<MainUI>();
        if (parent != null)
        {
            // A-0 : ���� ��ư�� ĳ���� �̸� ���� (ex Tony)
            parent.SetBuyButton(_CharacterNameValue.text);
            parent.otherScriptBtn = _btn;
            // A-2 : ���� ��ư�� ĳ���� �ǸŰ����� MainUI�� ����
            parent.OnClick_ShopUIBuyBtn(_PriceValue.text);
        }
        else
        {
            Debug.LogError("ParentObject script not found in parent hierarchy.");
        }

    }
    // Update is called once per frame
 
}
