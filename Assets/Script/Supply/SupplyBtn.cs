using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SupplyBtn : MonoBehaviour
{
    //false�� ����, true�� ������
    [SerializeField] private bool isRight;

    [SerializeField] private AudioClip pressSFX;
    [SerializeField] private AudioSource audioSource;

    public static event Action<bool> SupplyBtnPressed;

    public void btnPress()
    {
        this.audioSource.PlayOneShot(pressSFX);
        SupplyBtnPressed?.Invoke(isRight);
    }
}