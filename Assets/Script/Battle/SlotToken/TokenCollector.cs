using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollector : MonoBehaviour
{
    [SerializeField] private int tokenCount;

    private void OnEnable()
    {
        //turnStart�� ���缭 +=
    }

    private void OnDisable()
    {
        //turnStart�� ���缭 -=
    }

    




    private void Awake()
    {
        this.tokenCount = 4;
    }
}
