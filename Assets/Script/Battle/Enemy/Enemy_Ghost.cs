using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy_Base
{
    public Enemy_Ghost()
        : base(2, "����", "Enemy_Ghost")
    {
        EnemySkill skill1 = new EnemySkill(
            "��� ����Ű��", 0, 2, 1, 2, 0, 1, 1, 1, "������ �ϱ׷��� ������ ��򰡸� ����Ų��...");

        EnemySkill skill2 = new EnemySkill(
            "�߾�Ÿ���", 0, 4, 2, 0, 0, 3, 1, 0, "������ ���� �� ���� ���� �߾�Ÿ���...");

        /////////////////////////////////////

        EnemySkill skill3 = new EnemySkill(
            "������Ÿ���", 2, 5, 2, 0, 2, 2, 1, 0, "������ ���� ���� ���� �����Ÿ���...");

        EnemySkill skill4 = new EnemySkill(
            "���", 0, 6, 3, 0, 0, 4, 2, 0, "������ ������ ����� ��������.");

        List<EnemySkill> count1Skill = new List<EnemySkill> { skill1, skill2 };
        List<EnemySkill> count2Skill = new List<EnemySkill> { skill1, skill3, skill4 };

        this.skillList.Add(count1Skill);
        this.skillList.Add(count2Skill);
    }
}