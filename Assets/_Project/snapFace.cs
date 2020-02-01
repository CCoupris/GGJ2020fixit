using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapFace : MonoBehaviour
{
    public GameObject compatibleObject;

    private void Awake()
    {
        string searchname = gameObject.name.Replace("snap", "");
        compatibleObject = GameObject.Find(searchname);
    }
}
