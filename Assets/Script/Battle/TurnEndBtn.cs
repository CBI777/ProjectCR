using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnEndBtn : MonoBehaviour
{
    [SerializeField] private bool clicked;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private Button btn;
    [SerializeField] private Image img;

    private Color selClr = new Color(250f / 255f, 138f / 255f, 198f / 255f);

    public static event Action TurnEndEvent;

    private void OnEnable()
    {
        BattleResetManager.ResetBoardEvent += buttonReset;
    }

    private void OnDisable()
    {
        BattleResetManager.ResetBoardEvent -= buttonReset;
    }

    public void onClick()
    {
        if (clicked)
        {
            this.img.color = Color.white;
            this.btn.interactable = false;
            this.myText.SetText("��� ���� ����");
            TurnEndEvent?.Invoke();
        }
        else
        {
            this.clicked = true;
            this.myText.SetText("��� ���� Ȯ��");
            this.img.color = selClr;
        }
    }

    private void buttonReset()
    {
        this.btn.interactable = true;
        this.clicked = false;
        this.myText.SetText("�� ����");
        this.img.color = Color.white;
    }

    private void Start()
    {
        this.myText = this.transform.GetComponentInChildren<TextMeshProUGUI>();
        this.btn = this.transform.GetComponent<Button>();
        this.img = this.transform.GetComponent<Image>();
    }
}
