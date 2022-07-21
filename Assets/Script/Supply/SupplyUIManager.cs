using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;

    private void OnEnable()
    {
        SupplyManager.CurSupplyChanged += SupplyManager_CurSupplyChanged;
    }

    private void OnDisable()
    {
        SupplyManager.CurSupplyChanged -= SupplyManager_CurSupplyChanged;
    }

    private void SupplyManager_CurSupplyChanged(int curSupply, string supplyName, string batDescription)
    {
        //�̹��� ���� : �� ���, realname�� �޾ƾ��� ���� ����
        if (curSupply == 0) { this.prevBtn.SetActive(false); }
        else { if (!this.prevBtn.activeSelf) { this.prevBtn.SetActive(true); } }
        if (curSupply == 2) { this.nextBtn.SetActive(false); }
        else { if (!this.nextBtn.activeSelf) { this.nextBtn.SetActive(true); } }

        this.img.GetComponent<TooltipTrigger>().content = batDescription;
        this.img.GetComponent<TooltipTrigger>().header = supplyName;
    }
}
