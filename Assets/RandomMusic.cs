using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();

    private void Awake()
    {
        if(clips.Count != 0)
        {
            int index = (int)Random.value * clips.Count ;
            Debug.Log(index);
            GetComponent<AudioSource>().clip = clips[index];
            GetComponent<AudioSource>().Play();
        }
        
    }

}
