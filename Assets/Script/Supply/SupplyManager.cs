using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SupplyManager : MonoBehaviour
{
    private List<Supply_Base> supply = new List<Supply_Base>();
    [SerializeField] private int curSupply = 0;
    [SerializeField] private int supplyCount;

    //���� supply�� �ٲ������ �̾߱�����. �̰� UI�ʿ��� ���
    //ó�� int�� ��쿡�� �� ������ �ƴ����� �˷��ֱ� ����. 0�̸� prev�� ���ְ�, 1�̸� �׳� �ΰ�,
    //2���� next�� ���־��Ѵ�.
    public static event Action<int, string, string, string> CurSupplyChanged;
    //Supply ��ü�� ��ȭ�� ���� ���
    public static event Action<int, Supply_Base[]> SupplyChangedEvent;

    private void OnEnable()
    {
        SupplyBtn.SupplyBtnPressed += SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent += BattleResetManager_ResetBoardEvent;
    }

    private void OnDisable()
    {
        SupplyBtn.SupplyBtnPressed -= SupplyBtn_SupplyBtnPressed;
        BattleResetManager.ResetBoardEvent -= BattleResetManager_ResetBoardEvent;
    }

    private void BattleResetManager_ResetBoardEvent()
    {
        setSupply(0);
    }

    private void SupplyBtn_SupplyBtnPressed(bool obj)
    {
        if (obj) { setSupply((this.curSupply + 1)); }
        else { setSupply(this.curSupply - 1); }
    }

    private void setSupply(int n)
    {
        int temp = 0;

        if(n < 0 || n > supplyCount) { return; } //����ó����

        if(this.supply[curSupply].usage != 0) { this.supply[curSupply].onStopUse(); }
        this.curSupply = n;
        if (this.supply[curSupply].usage != 0) { this.supply[curSupply].onUse(); }

        if(curSupply == 0) { temp = 0; }
        else if (curSupply == this.supplyCount) { temp = 2; }
        else { temp = 1; }
        CurSupplyChanged?.Invoke(temp, this.supply[curSupply].realName, this.supply[curSupply].supplyName, this.supply[curSupply].batDescription);
    }

    private void turnStart()
    {
        this.supplyCount = (this.supply.Count - 1);
        SupplyChangedEvent?.Invoke(supplyCount, this.supply.ToArray());
        setSupply(0);
    }

    private void Start()
    {
        //�Ҹ�ǰ�� �ݵ�� NoUse �ϳ� �̻��� ������ �־���Ѵ�.
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_NoUse")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Opium")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Painkiller")));
        this.supply.Add((Supply_Base)Activator.CreateInstance(Type.GetType("Supply_Abhorrpainting")));
        turnStart();
    }
}
