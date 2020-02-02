using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticle : MonoBehaviour
{

    private void Awake()
    {
        splitObject.OnFinish += SplitObject_OnFinish;
    }

    private void SplitObject_OnFinish()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play(48000);
    }
}
