using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TotalDmgChange : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        SlotManager.TotalDmgChanged += SlotManager_TotalDmgChanged;
    }

    private void OnDisable()
    {
        SlotManager.TotalDmgChanged -= SlotManager_TotalDmgChanged;
    }

    private void SlotManager_TotalDmgChanged(int[] obj)
    {
        switch (typeNum)
        {
            case 0:
                myText.SetText(obj[0].ToString());
                break;
            case 1:
                myText.SetText(obj[1].ToString());
                break;
            case 2:
                myText.SetText(obj[2].ToString());
                break;
            case 3:
                myText.SetText(obj[3].ToString());
                break;
            case 4:
                myText.SetText(obj[4].ToString());
                break;
        }
    }
}
