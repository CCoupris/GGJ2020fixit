using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{

    static public int currentLevel;

    [SerializeField]
    protected Button next, previous, play;
    [SerializeField]
    protected GameObject vase;
    [SerializeField]
    protected List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    protected Transform vaseParent;
    [SerializeField]
    protected TextMeshProUGUI textPart;
    [SerializeField]
    protected List<string> parts = new List<string>();
    private void Awake()
    {
        play.onClick.AddListener(OnPlay);
        previous.onClick.AddListener(OnPrevious);
        next.onClick.AddListener(OnNext);
        CheckPot();
    }

    protected void OnNext()
    {
        currentLevel++;
        if(currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        CheckPot();
    }

    private void CheckPot()
    {
        if(vase != null)
        {
            Destroy(vase);
        }
        vase = Instantiate(levels[currentLevel], vaseParent);
        vase.transform.localPosition = Vector3.zero;
        textPart.text = parts[currentLevel];
    }

    protected void OnPrevious()
    {
        currentLevel--;
        if (currentLevel < 0)
        {
            currentLevel = levels.Count-1;
        }
        CheckPot();
    }

    protected void OnPlay()
    {
        SceneManager.LoadScene("level");
    }

    private void OnDestroy()
    {
        play.onClick.RemoveListener(OnPlay);
        previous.onClick.RemoveListener(OnPrevious);
        next.onClick.RemoveListener(OnNext);
    }
}
