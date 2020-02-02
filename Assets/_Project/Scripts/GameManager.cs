using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    protected PostProcessVolume winPostProd;
    [SerializeField]
    protected GameObject winScreen;
    private void Awake()
    {
        splitObject.OnFinish += SplitObject_OnFinish;
    }
    
    private void SplitObject_OnFinish()
    {
        winScreen.SetActive(true);
        Tween.Value(0f, 1f, updatePostProcess, 1, 1.5f, Tween.EaseInOut);
    }
    
    private void updatePostProcess(float value)
    {
        winPostProd.weight = value;
    }


    /*protected void updatePostProcess()
    {

    }*/

    private void OnDestroy()
    {
        splitObject.OnFinish -= SplitObject_OnFinish;

    }
}
