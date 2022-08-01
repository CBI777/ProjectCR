using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public enum DialogueProgress
{
    Bat, Skill, Status
};

public class BattleDialogueProvider : MonoBehaviour
{
    [SerializeField] DialogueProgress diaProg;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button proceedBtn;

    [SerializeField] private Battle_Script batScript;
    [SerializeField] private EnemySkillDialogue skillScript;
    //status script

    [SerializeField] private int bookmark = 0;
    [SerializeField] private int batBookmark = 0;
    [SerializeField] private int skillBookmark = 0;
    //bookmark = �� dialogue ������� å����
    //batbookmark = ���� / �� ���̿� ������ enemy ���� dialogue�� chapter�� ���δ� å����
    //skillScript = �� skill �ߵ��ÿ� ������ enemy�� skill dialogue�� chapter�� ���δ� å����

    public static event Action PrevDialogueDone;
    public static event Action betweenTurnDia;
    public static event Action turnStartDiaEnd;

    //�̰ɷ� �� ��°(int) ��ų�� dialogue�� ����� �������� �˷���.
    public static event Action<int> skillDiaStart;

    private void OnEnable()
    {
        nextBtn.interactable = false;
        proceedBtn.interactable = false;

        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent += TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass += SlotManager_FinalDmgPass;
        BattleDialogueProvider.betweenTurnDia += BattleDialogueProvider_betweenTurnDia;
    }

    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
        TurnEndBtn.TurnEndEvent -= TurnEndBtn_TurnEndEvent;
        SlotManager.TotalDmgPass -= SlotManager_FinalDmgPass;
        BattleDialogueProvider.betweenTurnDia -= BattleDialogueProvider_betweenTurnDia;
    }

    private void BattleDialogueProvider_betweenTurnDia()
    {
        scriptDisplay(batScript.script[batBookmark][bookmark]);
        bookmark++;

        if (batScript.script[batBookmark].Count() == 1)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void SlotManager_FinalDmgPass(int obj)
    {
        scriptDisplay(skillScript.skillDia[skillBookmark][bookmark]);
        scriptDisplay(false, "�̼��� " + obj + "�� ���ظ� �Ծ���!");


        if ((skillScript.skillDia[skillBookmark].Count() - 1) == bookmark)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void TurnEndBtn_TurnEndEvent()
    {
        //�̰� ���Դٴ� ���� ��� �������̶�� ��
        this.diaProg = DialogueProgress.Skill;
        skillDiaStart?.Invoke(bookmark);
    }

    private void scriptDisplay(scriptClass a)
    {
        if (a.isPorted)
        {
            DialogueManager.Instance.addDialogue(a.portName, a.isRight, a.line);
        }
        else
        {
            DialogueManager.Instance.addDialogue(a.isRight, a.line);
        }
    }
    private void scriptDisplay(SkillDialogue a)
    {
        DialogueManager.Instance.addDialogue(true, a.dialogue);
    }
    private void scriptDisplay(bool isRight, string portName, string line)
    {
        DialogueManager.Instance.addDialogue(portName, isRight, line);
    }
    private void scriptDisplay(bool isRight, string line)
    {
        DialogueManager.Instance.addDialogue(isRight, line);
    }

    private void SkillManager_enemyDecidedEvent(string obj)
    {
        //�̰� ���Դٴ� ���� �� ���� ���� ������ ���̶�� ��
        this.diaProg = DialogueProgress.Bat;
        this.batScript = Resources.Load<Battle_Script>("ScriptableObject/BattleScript/" + obj);
        this.skillScript = Resources.Load<EnemySkillDialogue>("ScriptableObject/EnemySkillDialogue/" + obj);

        scriptDisplay(batScript.script[batBookmark][bookmark]);
        bookmark++;

        if (batScript.script[batBookmark].Count() == 1)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void diaNextClicked()
    {
        if (this.diaProg == DialogueProgress.Bat)
        {
            scriptDisplay(batScript.script[batBookmark][bookmark]);
            bookmark++;
            if (batScript.script[batBookmark].Count() == bookmark)
            {
                proceedBtn.interactable = true;
                nextBtn.interactable = false;
            }
        }
        else if(this.diaProg == DialogueProgress.Skill)
        {
            bookmark++;
            skillDiaStart?.Invoke(bookmark);
        }
        else
        {
            /*status�� ����*/
        }
    }
    private void diaProceedClicked()
    {
        if (this.diaProg == DialogueProgress.Bat)
        {
            if (batBookmark == 0)
            {
                PrevDialogueDone?.Invoke();
            }
            else
            {
                DialogueManager.Instance.clearDialogue();
                turnStartDiaEnd?.Invoke();
            }
            //0���� �ƴ϶�� ���� turn�� �����ؾ���.

            batBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;
        }
        else if (this.diaProg == DialogueProgress.Skill)
        {
            skillBookmark++;
            bookmark = 0;
            proceedBtn.interactable = false;

            this.diaProg = DialogueProgress.Bat;
            betweenTurnDia?.Invoke();
        }
    }
}
