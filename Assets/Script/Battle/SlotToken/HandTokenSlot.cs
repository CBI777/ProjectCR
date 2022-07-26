using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandTokenSlot : MonoBehaviour, SlotInterface, IDropHandler
{
    //��ū�� ���� ������ onEndDrag�Ǿ��� ���
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<TokenDragDrop>().returnParent = this.transform;
        }
    }

    public void tokenAdded()
    {
        return;
    }
    public void tokenRemoved()
    {
        return;
    }
    public void tokenAfterRemoved()
    {
        return;
    }
}
