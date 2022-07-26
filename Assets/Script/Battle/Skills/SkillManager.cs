using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //1. skill�� �޾Ƽ� skill ���� �����ϸ鼭 �ѷ���
    //2. �� skill�� SlotManager�� skillBtn�� �޾Ƹ���

    //�̹� ���� EnemySkill + Skill ����
    public static event Action<List<EnemySkill>, int> SkillAddedEvent;
    //skillManager�� StageManager���׼� stage�� ������, �װſ� ���缭 skillmanager�� enemy�� ����, �׷��� BattleStart��.
    private StageVariations vari = new StageVariations();
    //�� int�� ���� ���� ���Ѱ���.
    public static event Action<int> BattleStart;

    [SerializeField] private Enemy_Base enemy;
    [SerializeField] private Image enemyPic;


    private void OnEnable()
    {
        StageManager.StageSpread += StageManager_StageSpread;
        TurnManager.TurnStart += TurnManager_TurnStart;
    }

    private void OnDisable()
    {
        StageManager.StageSpread -= StageManager_StageSpread;
        TurnManager.TurnStart -= TurnManager_TurnStart;
    }

    private void StageManager_StageSpread(int obj)
    {
        int temp = UnityEngine.Random.Range(1, 101);
        List<EnemyVariation> varis = vari.variations[obj];
        int i = 0;

        while(true)
        {
            if(temp < varis[i].num)
            {
                this.enemy = ((Enemy_Base)Activator.CreateInstance(Type.GetType(varis[i].realName)));
                this.enemyPic.sprite = Resources.Load<Sprite>("Sprite/Enemy/" + enemy.realName);
                BattleStart?.Invoke(this.enemy.turnCount);
                break;
            }
            i++;
        }
    }

    private void TurnManager_TurnStart(int obj)
    {
        skillSpread(obj);
    }

    public void skillSpread(int count)
    {
        SkillAddedEvent?.Invoke(enemy.skillList[count], enemy.skillList[count].Count);
    }

}
