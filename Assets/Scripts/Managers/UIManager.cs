using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 *  UIManager 존재 의의
 *  : sort order를 관리하기 위함
 *  
 */
public class UIManager 
{
    int _order = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();    

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Managers.Resource.Instantiate($"UI/Popup/{name}");

        return null;
    }

}
