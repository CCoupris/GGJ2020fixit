using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField]
    protected Button back;
    [SerializeField]
    protected Menu menu;
    private void Awake()
    {
        back.onClick.AddListener(OnBack);
    }

    private void OnBack()
    {
        gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(OnBack);

    }
}
