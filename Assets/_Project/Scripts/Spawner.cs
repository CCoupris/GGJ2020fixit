using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        Instantiate(levels[LevelSelector.currentLevel], transform);

    }
}
