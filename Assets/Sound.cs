using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    protected AudioSource tablepiece, takepiece, lastpiece, place;
    public static AudioSource Tablepiece, Takepiece, Lastpiece, Place;

    private void Awake()
    {
        Tablepiece = tablepiece;
        Takepiece = takepiece;
        Lastpiece = lastpiece;
        Place = place;
    }
}
