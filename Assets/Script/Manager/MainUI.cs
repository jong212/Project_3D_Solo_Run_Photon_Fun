using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public string CharacterPriceValue { get; set; }
    public string CharacterNameValue { get; set; }
    [SerializeField] private DBManager _DBManager;
    private void OnDisable()
    {
        UIManager.Instance.RegisterOnClickConfirmEvent(false, OnClickConfirmPopup);
    }

    // ���� UI ���� : ���� ������ Ŭ�� ��
    public void OnClick_ShopUIBtn()
    {
        UIManager.Instance.OpenShopPopupBtn();
    }

    // ���� UI ���� : ��������Ʈ�� BuyŬ�� ��
    public void OnClick_ShopUIBuyBtn()
    {
        
        UIManager.Instance.OpenShopBuyPopupBtn();

        // UI �Ŵ������� �ݹ��Լ� �Ѱ���
        // UIManager������ ���޹��� �ݹ��� ShopBuyYesBtn�� �ٽ� �����ؼ� ������
        UIManager.Instance.RegisterOnClickConfirmEvent(true, OnClickConfirmPopup);
    }
    // ���� UI ���� : Invoke �̺�Ʈ ȣ�� �� ��  (���� BuyPopup���� �� ��ư ���� ��)
    public void OnClickConfirmPopup()
    {
        // ���� �����ϱ� �� ��ư�� ������ ĳ�����̸����� DB�� ���޽�Ŵ > DB������ �÷��̾� �г��ӿ� �ش��ϴ� ������ ĳ���� ������ �̸��� insert
        _DBManager.InsertCharacterInfo(CharacterNameValue);

    }
    // ���� UI ���� : 
    public void SetBuyButton(string characterNameValue)
    {
        CharacterNameValue = characterNameValue;
    }

    // ĳ���� UI ���� 
    // UIManager ���� ��û
    public void OnClick_CharacterIconBtn()
    {
        UIManager.Instance.OpenCharacterPopup();
    }
}
