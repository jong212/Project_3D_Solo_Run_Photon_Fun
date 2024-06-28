using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] Text currentMoney; // UI MOney
    public string CharacterNameValue { get; set; }
    public int CharacterPriceValue { get; set; }
    [SerializeField] private DBManager _DBManager;
    private void OnDisable()
    {
        UIManager.Instance.RegisterOnClickConfirmEvent(false, OnClickConfirmPopup);
    }

    // ����UI ������ Ŭ�� ��
    public void OnClick_ShopUIBtn()
    {
        UIManager.Instance.OpenShopPopupBtn();
    }

    // ������������ Buy ��ư Ŭ�� ��
    public void OnClick_ShopUIBuyBtn(string price)
    {
        //to int �� change
        if (int.TryParse(price, out int priceValue))
        {
            CharacterPriceValue = priceValue;
            UIManager.Instance.OpenShopBuyPopupBtn();

            // UI �Ŵ������� �ݹ��Լ� �Ѱ���        
            UIManager.Instance.RegisterOnClickConfirmEvent(true, OnClickConfirmPopup);
        }

    }
    // ����ڰ� �����ϱ� �˾����� Yes�� Ŭ������ �� Invoke�� ���� ��/
    public void OnClickConfirmPopup()
    { 
                
        if(currentMoney != null){
       
                int currentMoneyInt = Convert.ToInt32(currentMoney.text);
            // ����ڰ� ������ ������ ������ DB���� �÷��̾��� �����ݾ��� select �ؿ� �� ���
            // �α��� �� �� 1ȸ�� select �ؿͼ� �����ݾ� �޾ƿ��� CurrentGold�� �־�� ����
            // �ΰ��ӿ��� ���Ű� �Ͼ�� �ϴ��� CurrentGold�� UI�� currentMoneyInt ���� �������� ��

            
            if (_DBManager.CurrentGold == currentMoneyInt)
            {   
                // ���ٸ� ���� ���� �ݾ׿��� ������ ĳ���� �ݾ��� �������Ѽ� ���� �������� üũ
                var tempCalcValue = currentMoneyInt - CharacterPriceValue;
                if (tempCalcValue < 0)
                {
                    Debug.Log("���� ������");
                } 
                else
                {
                    _DBManager.CurrentGold = _DBManager.CurrentGold - CharacterPriceValue;
                    _DBManager.UpdatePlayerGold(_DBManager.CurrentGold);
                    currentMoney.text = _DBManager.CurrentGold.ToString();
                }
            }
            else
            {
                Debug.Log("The current money does not match the DB value.");
            }
        }
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
