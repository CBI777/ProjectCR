using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleDialogueProvider : MonoBehaviour
{
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button proceedBtn;

    [SerializeField] private Battle_Script batScript;
    [SerializeField] private ListWrapper<scriptClass> scriptClasses;

    [SerializeField] private int bookmark = 0;
    [SerializeField] private int bigBookmark = 0;
    //������ ������ bigbookmark�� reset.
    //proceed�� ������ bookmark�� reset.

    public static event Action PrevDialogueDone;

    private void OnEnable()
    {
        nextBtn.interactable = false;
        proceedBtn.interactable = false;

        SkillManager.enemyDecidedEvent += SkillManager_enemyDecidedEvent;
        //turnend���� count�� ���缭 dialogue�� ���۵Ǿ�ߵ�.
    }
    private void OnDisable()
    {
        SkillManager.enemyDecidedEvent -= SkillManager_enemyDecidedEvent;
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

    private void SkillManager_enemyDecidedEvent(string obj)
    {
        this.batScript = Resources.Load<Battle_Script>("ScriptableObject/BattleScript/" + obj);
        this.scriptClasses = batScript.script[bigBookmark];

        scriptDisplay(scriptClasses[bookmark]);
        bookmark++;

        if (scriptClasses.Count() == 1)
        {
            proceedBtn.interactable = true;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }

    private void diaNextClicked()
    {
        scriptDisplay(scriptClasses[bookmark]);
        bookmark++;
        if(scriptClasses.Count() == bookmark)
        {
            proceedBtn.interactable = true;
            nextBtn.interactable = false;
        }
    }
    private void diaProceedClicked()
    {
        if(bigBookmark == 0)
        {
            PrevDialogueDone?.Invoke();
        }
        //0���� �ƴ϶�� ���� turn�� �����ؾ���.

        bigBookmark++;
        bookmark = 0;
        proceedBtn.interactable = false;
    }
}
