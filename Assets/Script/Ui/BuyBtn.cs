using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ĳ���� �����ϱ� ��ư�� ������ ĳ���� �̸� Value�� MainUI�� �������ش�


public class BuyBtn : MonoBehaviour
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
            parent.SetBuyButton(_CharacterNameValue.text);


            parent.OnClick_ShopUIBuyBtn();
        }
        else
        {
            Debug.LogError("ParentObject script not found in parent hierarchy.");
        }

    }
    // Update is called once per frame

}
