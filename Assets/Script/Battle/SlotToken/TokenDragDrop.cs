using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TokenDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform Hand;

    public Transform returnParent;
    private Transform beforeParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] private bool dragable = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        this.Hand = GameObject.FindWithTag("Hand").transform;
        this.beforeParent = this.transform.parent;
        this.returnParent = this.transform.parent;

        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
    }
    private void OnDisable()
    {
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
    }

    private void BattleResetManager_ResetBoardEvent()
    {
        this.transform.SetParent(this.Hand); //1. �ڵ�� �ű��.
        tokenRemoveCheck(); //2.
        /* ������ ũ�� �� ������ ������ ��ģ��.
         * 1) Slot���� �ٽ� calculation�� �ϵ��� �Ѵ�.
         * 2) ��� slot�� enable��Ų��.
         */
        this.returnParent = this.transform.parent;
        this.beforeParent = this.transform.parent; //3. before�� return�� �������´�.
        this.Dragable();
    }

    private void tokenRemoveCheck()
    {
        this.returnParent.SendMessage("tokenRemoved");
    }

    private void tokenAddCheck()
    {
        this.returnParent.SendMessage("tokenAdded");
    }

    private void tokenAfterRemoveCheck()
    {
        this.beforeParent.SendMessage("tokenAfterRemoved");
    }

    //OnPointerDown->OnBeginDrag->OnDrag->OnDrop->OnEndDrag

    //��ū�� ���� ������ ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!this.dragable) { return; }

        this.returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        tokenRemoveCheck();
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    //��ū�� ���� ���� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (!this.dragable) { return; }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //��ū�� drop�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!this.dragable) { return; }

        this.transform.SetParent(this.returnParent);
        tokenAddCheck();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(this.returnParent != this.beforeParent)
        {
            tokenAfterRemoveCheck();
        }
        this.beforeParent = this.transform.parent;
    }

    //��ū Ŭ��(��������) �� ��
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!this.dragable) { return; }
    }

    public void Undragable()
    {
        this.dragable = false;
        this.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/TokenB");
    }

    public void Dragable()
    {
        this.dragable = true;
        this.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/UI/TokenN");
    }
}
