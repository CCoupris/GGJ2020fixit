using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class splitObject : MonoBehaviour
{
    protected List<snapFace> snapFaces = new List<snapFace>();
    [SerializeField]
    protected bool isFixed;
    protected bool rotatingMode;
    protected new OrbitalCamera camera;
    protected Orbit orbit;
    private bool inHud;

    public bool IsFixed 
    { 
        get => isFixed;
        set
        {
            isFixed = value;
            CheckFace();
        }
    }

    private void CheckFace()
    {
        foreach (snapFace face in snapFaces)
        {
            if (isFixed)
            {
                face.gameObject.SetActive(true);
            }
            else
            {
                face.gameObject.SetActive(false);

            }
        }
    }

    private void Awake()
    {
        snapFaces = GetComponentsInChildren<snapFace>().ToList();
        orbit = GetComponent<Orbit>();
        CheckFace();
        camera = FindObjectOfType<OrbitalCamera>();
    }

    protected void OnMouseOver()
    {
        if (InputManager.Click)
        {
            if (!IsFixed)
            {
                if (!rotatingMode)
                {
                    inHud = false;
                    orbit.enabled = true;
                    camera.enabled = false;
                    rotatingMode = true;
                    transform.parent = GameObject.FindGameObjectWithTag("ObjectContainer").transform;
                    StartCoroutine(GoToPoint(GameObject.FindGameObjectWithTag("RotationPosition").transform.position, 2));
                    gameObject.layer = 5;
                }
                else
                {
                    rotatingMode = false;
                    camera.enabled = true;
                    orbit.enabled = false;
                    StartCoroutine(GoInHUD(2));
                }
            }
        }
    }

    private IEnumerator GoInHUD(float transitionTime)
    {
        Transform position = GameObject.FindGameObjectWithTag("CubePosition").transform;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(transform.position, position.position, time);
            yield return null;
        }
        inHud = true;
    }

    private void Update()
    {
        if (inHud)
        {
            if (InputManager.Click)
            {
                int layer = 1 << 5 | 1 << 2;
                layer = ~layer;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, layer))
                {
                    if (hit.transform.GetComponent<snapFace>())
                    {
                        if (hit.transform.GetComponent<snapFace>().compatibleObject == gameObject)
                        {
                            transform.parent = null;
                            StartCoroutine(GoToPoint(hit.transform.position, 2, hit.transform.rotation));
                            gameObject.layer = 9;
                            IsFixed = true;
                        }
                    }
                }
            }
        }
    }

    protected IEnumerator GoToPoint(Vector3 position, float transitionTime)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(transform.position, position, time);
            yield return null;
        }
    }

    protected IEnumerator GoToPoint(Vector3 position, float transitionTime, Quaternion rotation)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(transform.position, position, time);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, time);
            yield return null;
        }
    }

    protected IEnumerator GoToRotationCubeMode(float transitionTime)
    {
        Transform position = GameObject.FindGameObjectWithTag("RotationPosition").transform;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(transform.position, position.position, time);
            yield return null;
        }
    }

    private void OnDestroy()
    {

    }
}
