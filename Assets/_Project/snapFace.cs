using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapFace : MonoBehaviour
{
    public GameObject compatibleObject;

    private void Awake()
    {
        compatibleObject = GameObject.Find("/" + gameObject.name);
    }
}
