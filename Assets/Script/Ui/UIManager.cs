using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    ConfirmPopup,
    MainUI,

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
    private void OpenUI(UIType uiType, GameObject uiObject)
    {
        if (_openedUIDic.Contains(uiType) == false)
        {
            //OpenUI�� �ٷ� Ÿ�� ���̽��� �־ ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� Ȱ��ȭ ��Ű�� �;
            uiObject.SetActive(true);
            _openedUIDic.Add(uiType);
        }
    }

    private void CloseUI(UIType uiType)
    {
        if (_openedUIDic.Contains(uiType))
        {
            var uiObject = _createdUIDic[uiType];
            uiObject.SetActive(false);
            _openedUIDic.Remove(uiType);
        }
    }

    private void CreateUI(UIType uiType)
    {
        if (_createdUIDic.ContainsKey(uiType) == false)
        {
            string path = GetUIPath(uiType);
            GameObject loadedObj = (GameObject)Resources.Load(path);
            GameObject gObj = Instantiate(loadedObj, UIRoot.transform);
            if (gObj != null)
            {
                _createdUIDic.Add(uiType, gObj);
            }
        }
    }

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
            case UIType.ConfirmPopup:
                path = "UI/LobbyShopPopupUI";
                break;
        }

        return path;
    }
    public void CloseSpecificUI(UIType uiType)
    {
        CloseUI(uiType);
    }

    // Ȯ�尡��
    public void OpenConfirmBtn(string msg)
    {
        var gObj = GetCreatedUI(UIType.ConfirmPopup);

        if (gObj != null)
        {
            OpenUI(UIType.ConfirmPopup, gObj);
            // _simplePopup.gameObject.SetActive(true); -> OpenUI�� ���� ����

             var ConfirmButton = gObj.GetComponent<ConfirmButton>();
        }
    }

    public void RegisterOnClickConfirmEvent(bool isRegister, Action callback)
    {
        if (_createdUIDic.ContainsKey(UIType.ConfirmPopup))
        {
            var gObj = _createdUIDic[UIType.ConfirmPopup];
            var confirmPopup = gObj.GetComponent<ConfirmButton>();
            confirmPopup?.RegisterOnClickConfirmEvent(isRegister, callback);
        }
    }


}
