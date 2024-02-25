using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action <Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void onUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // UIŬ���� ��Ȳ�̸� ����
            return;

        if (Input.anyKey && KeyAction != null ) //anykey�� ���콺�� Ű �� �� �ν�
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0)) // ���콺 ��ư ����
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true; // 0.2�� �̻� ���ӵǸ� drag�� �ǰ�
                                 // drag�� �߰��Ҽ�������
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }
}
