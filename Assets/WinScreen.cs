using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    [SerializeField]
    protected Button restart, menu;
    protected CanvasGroup canvas;
    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        restart.onClick.AddListener(OnRestart);
        menu.onClick.AddListener(OnMenu);
    }

    private void Start()
    {
        Tween.Value(0f, 1f, ChangeAlpha, 1, 2, Tween.EaseInOut);
    }

    private void ChangeAlpha(float value)
    {
        canvas.alpha = value;
    }

    private void OnMenu()
    {
        SceneManager.LoadScene("menu");

    }

    private void OnRestart()
    {
        SceneManager.LoadScene("level");
    }


}
