using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SupplyBtn : MonoBehaviour
{
    //false�� ����, true�� ������
    [SerializeField] private bool isRight;
    public static event Action<bool> SupplyBtnPressed;

    public void btnPress()
    {
        SupplyBtnPressed?.Invoke(isRight);
    }
}