using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    protected enum menuButtonState
    {
        play,
        option,
        credit,
        quit
    }

    [SerializeField]
    protected Button play, option, quit, credit;

    [SerializeField]
    protected Button previous, next;


    private menuButtonState buttonState;

    protected menuButtonState ButtonState 
    { 
        get => buttonState; 
        set => buttonState = value; 
    }

    protected void Awake()
    {
        play.onClick.AddListener(OnPlay);
        play.onClick.AddListener(OnOption);
        quit.onClick.AddListener(OnQuit);
        credit.onClick.AddListener(OnCredit);
        previous.onClick.AddListener(OnPrevious);
        next.onClick.AddListener(OnNext);
    }

    private void OnNext()
    {

    }

    private void OnPrevious()
    {

    }

    private void OnCredit()
    {
        
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnOption()
    {

    }

    private void OnPlay()
    {

    }
}
