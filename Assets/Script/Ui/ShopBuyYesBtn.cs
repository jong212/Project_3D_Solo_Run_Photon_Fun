using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// A ���� �� �� �̺�Ʈ ���
// B �������� Buy ��ư Ŭ�� �� Yes or No �ܰ迡�� Yes ���� �� OnClick_Confirm �κ�ũ ����
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
