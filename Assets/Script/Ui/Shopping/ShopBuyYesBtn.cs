using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// A ���� �� �� �̺�Ʈ�� �̸� ���
// B ���� �����˾����� Yes ��ư ���� �� �κ�ũ ����
public class ShopBuyYesBtn : MonoBehaviour
{
    public Button _btn;
    public Action _onConfirmEventHandler;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClick_Confirm);
    }
    private void OnDisable()
    {
        if (_onConfirmEventHandler != null)
            _onConfirmEventHandler = null;
    }
    //A
    public void RegisterOnClickConfirmEvent(bool isRegister, Action callback)
    {
        if (isRegister)
            _onConfirmEventHandler += callback;
        else
            _onConfirmEventHandler -= callback;
    }
    //B
    public void OnClick_Confirm()
    {
        _onConfirmEventHandler?.Invoke();
    }
}
