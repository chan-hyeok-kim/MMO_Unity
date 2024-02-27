using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
/*       invoke의 경우,
 *       멀티 스레드 환경에서 동시 작업은 아닌 채로 작업하게 된다
 *       즉, 메인 스레드는 멈춰두고 invoke가 별도의 스레드에서 실행됨
 */
            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler != null)
           OnDragHandler.Invoke(eventData);

    }
}
