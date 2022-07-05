using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TokenSlot : MonoBehaviour, IDropHandler
{
    public enum SlotType
    {
        Hand,
        Resist,
        Equipment,
        Enemy
    };
    [SerializeField] private SlotType slotType;
    [SerializeField] private GameObject textObject;
    [SerializeField] private bool slotEnabled;
    private TMP_Text matchingText;
    private int tokenCount = 0;

    //���� ���ۿ� ������ ��ū ������ üũ�ϴ� �Լ���
    //��, �� ��ū�� ���̴� ���� �ִٸ�(�������) �� ��ū�� ���Ƶ� �ڿ� �̰� �θ��� ��.
    public void initSlot()
    {
        this.tokenCount = this.transform.childCount;
        this.matchingText = textObject.GetComponent<TMP_Text>();
        matchingText.text = tokenCount.ToString();
    }

    public void Start()
    {
        //Temporary
        initSlot();
    }

    //��ū�� ���� ������ onEndDrag�Ǿ��� ���
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && slotEnabled)
        {
            Debug.Log("On Drop" + eventData.pointerDrag.name);
            eventData.pointerDrag.GetComponent<TokenDragDrop>().newParent = this.transform;
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        switch (slotType)
        {
            case SlotType.Enemy:
            case SlotType.Hand:
                break;
            case SlotType.Resist:
                this.tokenCount++;
                matchingText.text = tokenCount.ToString();
                break;
            case SlotType.Equipment:
                //TODO
                break;
            default:
                break;
        }
    }

    public void OnBeginDrag()
    {
        if (slotType == SlotType.Resist)
        {
            this.tokenCount--;
            matchingText.text = tokenCount.ToString();
        }
    }

    public void OnCancelDrag()
    {
        if (slotType == SlotType.Resist)
        {
            this.tokenCount++;
            matchingText.text = tokenCount.ToString();
        }
    }
}
