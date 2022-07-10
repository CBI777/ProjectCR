using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EquipmentChangeBtn : MonoBehaviour
{
    public static event Action<int> OnECBtnClick;
    public static event Action<int> OnEquipChange;

    [SerializeField] private GameObject eqName;
    [SerializeField] private GameObject phys;
    [SerializeField] private GameObject fear;
    [SerializeField] private GameObject abhorr;
    [SerializeField] private GameObject delus;
    [SerializeField] private GameObject eqImg;

    [SerializeField] private int num;
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI myText;

    //cur�� ���� �̰� ���� �־��°�? sel�� ���� ���õǾ��ִ°�
    //exist�� ���ʿ� �� ��� �ִ����� Ȯ��
    [SerializeField] private bool cur;
    [SerializeField] private bool sel;
    [SerializeField] private bool exist;

    private void OnEnable()
    {
        this.myText = this.transform.GetComponentInChildren<TextMeshProUGUI>();
        this.btn = this.transform.GetComponent<Button>();
        EquipmentChangeBtn.OnECBtnClick += EquipmentChangeBtn_OnECBtnClick;
        EquipmentChangeBtn.OnEquipChange += EquipmentChangeBtn_OnEquipChange;
        EquipmentManager.EquipChangedEvent += EquipmentManager_EquipChangedEvent;
        EquipmentManager.CurEquipChanged += EquipmentManager_CurEquipChanged;
    }

    private void OnDisable()
    {
        EquipmentChangeBtn.OnECBtnClick -= EquipmentChangeBtn_OnECBtnClick;
        EquipmentChangeBtn.OnEquipChange -= EquipmentChangeBtn_OnEquipChange;
        EquipmentManager.CurEquipChanged -= EquipmentManager_CurEquipChanged;
        EquipmentManager.EquipChangedEvent -= EquipmentManager_EquipChangedEvent;
    }

    private void EquipmentManager_CurEquipChanged(int arg1, int arg2)
    {
        if (this.num == arg1)
        {
            this.cur = true; this.sel = true;
            this.myText.SetText("������");
            this.btn.interactable = false;
        }
        else
        {
            this.cur = false; this.sel = false;
            this.myText.SetText("����");
            this.btn.interactable = true;
        }
    }

    private void EquipmentManager_EquipChangedEvent(int arg1, int arg2, Equipment[] arg3)
    {
        if(this.num < arg2)
        {
            this.exist = true;
            this.eqName.GetComponent<TextMeshProUGUI>().SetText(arg3[this.num].equipName);
            this.phys.GetComponent<TextMeshProUGUI>().SetText(arg3[this.num].resChange[0].ToString());
            this.fear.GetComponent<TextMeshProUGUI>().SetText(arg3[this.num].resChange[1].ToString());
            this.abhorr.GetComponent<TextMeshProUGUI>().SetText(arg3[this.num].resChange[2].ToString());
            this.delus.GetComponent<TextMeshProUGUI>().SetText(arg3[this.num].resChange[3].ToString());
            //eqImg
            EquipmentManager_CurEquipChanged(arg1, arg2);
        }
        else
        {
            this.exist = false;
            this.eqName.GetComponent<TextMeshProUGUI>().SetText("��� ����");
            this.phys.GetComponent<TextMeshProUGUI>().SetText("-");
            this.fear.GetComponent<TextMeshProUGUI>().SetText("-");
            this.abhorr.GetComponent<TextMeshProUGUI>().SetText("-");
            this.delus.GetComponent<TextMeshProUGUI>().SetText("-");

            this.cur = false; this.sel = false;
            this.myText.SetText("-");
            this.btn.interactable = false;
        }
    }

    private void EquipmentChangeBtn_OnEquipChange(int obj)
    {
        this.btn.interactable = false;
    }

    private void EquipmentChangeBtn_OnECBtnClick(int a)
    {
        //�ٸ� ��ư�� ���ȴٸ�
        if(this.num != a && exist)
        {
            this.sel = false;
            this.btn.interactable = true;
            if(cur) { this.myText.SetText("���"); }
            else { this.myText.SetText("����"); }
        }
    }

    public void clickBtn()
    {
        if(!cur)
        {
            //���� ���� ���� �ƴϾ��µ�, sel�� ���¿��� �� �� �� �����ٸ� �ٲ۴ٴ°�
            if(sel)
            {
                OnEquipChange?.Invoke(this.num);
            }
            else
            {
                //���� ���� ���� �ƴϰ�, sel�� �ƴϾ��ٸ� �� �� ���ٴ°�
                OnECBtnClick?.Invoke(this.num);
                this.sel = true;
                this.myText.SetText("Ȯ��");
            }
        }
        //cur�� ���õȴٴ� ����, �ٸ� �ɷ� �ٲ�ٰ� �ٽ� ���ƿԴٴ°�
        else
        {
            //�ٽ� ���� status�� revert�ϴ°�
            OnECBtnClick?.Invoke(this.num);
            this.sel = true;
            this.btn.interactable = false;
            this.myText.SetText("������");
        }
    }
}
