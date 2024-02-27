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
/*       invoke�� ���,
 *       ��Ƽ ������ ȯ�濡�� ���� �۾��� �ƴ� ä�� �۾��ϰ� �ȴ�
 *       ��, ���� ������� ����ΰ� invoke�� ������ �����忡�� �����
 */
            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler != null)
           OnDragHandler.Invoke(eventData);

    }
}
