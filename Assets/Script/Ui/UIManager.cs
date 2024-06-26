using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    MainUI,
    ShopPopup,
    BuyPopup,

}


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject UIRoot;


    public static UIManager Instance { get; set; }

    // ��� ������ ���ſ� ���� �κ� -> Instancing�� �������÷��Ϳ� ������ �ִ� ��
    private Dictionary<UIType, GameObject> _createdUIDic = new Dictionary<UIType, GameObject>();
    // ��� Ȱ���� ��Ȱ���� ���� �κ� -> SetActive
    private HashSet<UIType> _openedUIDic = new HashSet<UIType>();

    private void Awake()
    {
        Instance = this;
    }

    // ����
    // �����ְ� 
    private void OpenUI(UIType uiType, GameObject uiObject)
    {
        if (_openedUIDic.Contains(uiType) == false)
        {
            //OpenUI�� �ٷ� Ÿ�� ���̽��� �־ ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� Ȱ��ȭ ��Ű�� �;
            uiObject.SetActive(true);
            _openedUIDic.Add(uiType);
        }
    }

    // ����
    // �ݾ��ְ� 
    private void CloseUI(UIType uiType)
    {
        if (_openedUIDic.Contains(uiType))
        {
            var uiObject = _createdUIDic[uiType];
            uiObject.SetActive(false);
            _openedUIDic.Remove(uiType);
        }
    }
    

    // ����
    // �������ְ�  
    private void CreateUI(UIType uiType)
    {
        if (_createdUIDic.ContainsKey(uiType) == false)
        {
            string path = GetUIPath(uiType);
            GameObject loadedObj = (GameObject)Resources.Load(path);

            GameObject gObj = null; 
            if (uiType == UIType.ShopPopup)
            {
                gObj = Instantiate(loadedObj, UIRoot.transform);
            } else if (uiType == UIType.BuyPopup)
            {
                gObj = Instantiate(loadedObj, UIRoot.GetComponentInChildren<ShopPopupUI>().transform);
            }

            
            if (gObj != null)
            {
                _createdUIDic.Add(uiType, gObj);
            }
        }
    }

    // ����
    // ����� �޶� ��û���ְ� 
    private GameObject GetCreatedUI(UIType uiType)
    {
        if (_createdUIDic.ContainsKey(uiType) == false)
        {
            CreateUI(uiType);
        }

        return _createdUIDic[uiType];
    }
    private string GetUIPath(UIType uiType)
    {
        string path = string.Empty; // "" == string.Empty
        switch (uiType)
        {
    // Ȯ�尡��
            case UIType.ShopPopup:
                path = "UI/LobbyShopPopupUI";
                break;
            case UIType.BuyPopup:
                path = "UI/BuyPopup";
                break;
        }

        return path;
    }
    public void CloseSpecificUI(UIType uiType)
    {
        CloseUI(uiType);
    }

    // �ܵ� �Լ� (Ȯ�� ����)
    public void OpenShopPopupBtn()
    {
        // ���̾��Ű�� �����ϴ��� üũ
        var gObj = GetCreatedUI(UIType.ShopPopup);
        if (gObj != null)
        {   // ������Ʈ Ȱ��ȭ ��û
            OpenUI(UIType.ShopPopup, gObj);
             var ConfirmButton = gObj.GetComponent<ShopPopupUI>();
        }
    }
    // �ܵ� �Լ� (Ȯ�� ����)
    public void OpenShopBuyPopupBtn()
    {
        // ���̾��Ű�� �����ϴ��� üũ
        var gObj = GetCreatedUI(UIType.BuyPopup);
        if (gObj != null)
        {   // ������Ʈ Ȱ��ȭ ��û
            OpenUI(UIType.BuyPopup, gObj);
            var ConfirmButton = gObj.GetComponent<ShopPopupUI>();
        }
    }
    
    public void RegisterOnClickConfirmEvent(bool isRegister, Action callback)
    {
        if (_createdUIDic.ContainsKey(UIType.ShopPopup))
        {
            var gObj = _createdUIDic[UIType.ShopPopup];
            var ShopPopup = gObj.GetComponent<ShopPopupUI>();
            ShopPopup?.RegisterOnClickConfirmEvent(isRegister, callback);
        }
    }


}
