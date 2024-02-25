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
        if (EventSystem.current.IsPointerOverGameObject()) // UI클릭된 상황이면 리턴
            return;

        if (Input.anyKey && KeyAction != null ) //anykey는 마우스랑 키 둘 다 인식
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0)) // 마우스 버튼 누름
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true; // 0.2초 이상 지속되면 drag가 되게
                                 // drag를 추가할수도있음
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
