using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//(�̺�Ʈ ���� ���� ����) ���� Ui ���� ��ư Ŭ��(���� Ÿ����) > UI �Ŵ������� �ݹ��Լ� �Ѱ��� > UI �Ŵ����� ���� �ݹ��Լ��� �Ŵ������� ���� ó����  ������ �ʰ� ���� UI�� ���̾��Ű�� �ִ��� üũ�� �ϰ� �ִٸ� "���� UI ���� �ݹ��� �ٽ� �ѱ�"�鼭 > ���� UI������ �Ѱܹ��� �ݹ��Լ��� += ��ϸ� ���ѳ��� > ���� UI ���� Ư�� ��ư�� ������ += �س����� �κ�ũ �� �� �ִ� �Լ� ����� ���Ḹ �ϸ� ��
public class MainUI : MonoBehaviour
{
    //BuyBtn ���� �������� ���� ��ư Ŭ������ �� �Ʒ� �־����
    public GameObject BuyBtn { get; set; }

    private void OnDisable()
    {
        UIManager.Instance.RegisterOnClickConfirmEvent(false, OnClickConfirmPopup);
    }


    public void OnClick_ShopUIBtn()
    {
        UIManager.Instance.OpenShopPopupBtn();
    }
    public void OnClick_ShopUIBuyBtn()
    {
        Debug.Log("test");
        UIManager.Instance.OpenShopBuyPopupBtn();
        UIManager.Instance.RegisterOnClickConfirmEvent(true, OnClickConfirmPopup);//ToDO �̰� Buy ��ư ���� �� ó���Ǵ� �ݹ��� ���� �����ؾ��ҵ�
    }

    public void OnClickConfirmPopup()
    {
        Debug.Log("...");
    }
    public void SetBuyButton(GameObject button)
    {
        BuyBtn = button;
        Debug.Log(button);
    }
}
