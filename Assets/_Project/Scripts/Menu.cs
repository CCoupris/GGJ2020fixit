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
    [SerializeField]
    protected GameObject optionCard, creditCard, playCard;

    private menuButtonState buttonState;

    protected menuButtonState ButtonState 
    { 
        get => buttonState;
        set 
        {
            if(value < 0)
            {
                buttonState = (menuButtonState)3;
            } else if((int)value > 3)
            {
                buttonState = (menuButtonState)0;
            } else
            {
                buttonState = value;
            }

            CheckState();
        }
    }

    private void CheckState()
    {
        if(buttonState == menuButtonState.play)
        {
            play.gameObject.SetActive(true);
            option.gameObject.SetActive(false);
            credit.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
        } else if (buttonState == menuButtonState.option)
        {
            credit.gameObject.SetActive(false);
            play.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            option.gameObject.SetActive(true);

        }
        else if (buttonState == menuButtonState.credit)
        {
            play.gameObject.SetActive(false);
            option.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            credit.gameObject.SetActive(true);

        }
        else if (buttonState == menuButtonState.quit)
        {
            play.gameObject.SetActive(false);
            option.gameObject.SetActive(false);
            credit.gameObject.SetActive(false);
            quit.gameObject.SetActive(true);
        }
    }

    protected void Awake()
    {
        play.onClick.AddListener(OnPlay);
        option.onClick.AddListener(OnOption);
        quit.onClick.AddListener(OnQuit);
        credit.onClick.AddListener(OnCredit);
        previous.onClick.AddListener(OnPrevious);
        next.onClick.AddListener(OnNext);
        CheckState();
    }

    private void OnNext()
    {
        ButtonState++;
    }

    private void OnPrevious()
    {
        ButtonState--;
    }

    private void OnCredit()
    {
        gameObject.SetActive(false);
        creditCard.SetActive(true);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnOption()
    {
        gameObject.SetActive(false);
        optionCard.SetActive(true);
    }

    private void OnPlay()
    {
        gameObject.SetActive(false);
        playCard.SetActive(true);
    }

    private void OnDestroy()
    {
        play.onClick.RemoveListener(OnPlay);
        option.onClick.RemoveListener(OnOption);
        quit.onClick.RemoveListener(OnQuit);
        credit.onClick.RemoveListener(OnCredit);
        previous.onClick.RemoveListener(OnPrevious);
        next.onClick.RemoveListener(OnNext);
    }
}
