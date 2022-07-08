using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTokenSlot : MonoBehaviour, IDropHandler
{
    //��ū�� ���� ������ onEndDrag�Ǿ��� ���
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("On Drop" + eventData.pointerDrag.name);
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }
}
