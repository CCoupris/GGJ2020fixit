using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Orbit))]
public class splitObject : MonoBehaviour
{
    protected List<snapFace> snapFaces = new List<snapFace>();
    [SerializeField]
    protected bool isFixed;
    protected bool rotatingMode;
    protected new OrbitalCamera camera;
    protected Orbit orbit;
    private bool inHud;
    protected static splitObject inHand;
    protected bool drag;
    protected Vector3 initialPos;
    protected Quaternion initialRot;
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
                face.CheckCompatible();
            }
            else
            {
                face.gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        transform.parent = null;
        snapFaces = GetComponentsInChildren<snapFace>().ToList();
        orbit = GetComponent<Orbit>();
        CheckFace();
        camera = FindObjectOfType<OrbitalCamera>();
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    protected void OnMouseOver()
    {
        if (InputManager.Click)
        {
            if (inHand == null || inHand == this)
            {
                if (!IsFixed)
                {
                    if (!rotatingMode)
                    {
                        inHand = this;
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
        if (InputManager.StartTouch)
        {
            if (inHand == this)
            {
                drag = true;
                camera.enabled = false;
                orbit.enabled = true;
            }
            
        }
    }

    private IEnumerator GoInHUD(float transitionTime)
    {
        Transform position = GameObject.FindGameObjectWithTag("CubePosition").transform;
        transform.parent = position;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, time);
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
                            inHud = false;
                            inHand = null;
                            gameObject.layer = 9;
                            transform.parent = null;
                            StartCoroutine(GoToPoint(hit.transform.position, 2, hit.transform.rotation));
                            IsFixed = true;
                            foreach (snapFace face in snapFaces)
                            {
                                face.gameObject.SetActive(true);
                                face.CheckCompatible();
                            }
                        }

                    } else if(!hit.transform.GetComponent<splitObject>())
                    {
                        inHud = false;
                        inHand = null;
                        gameObject.layer = 9;
                        transform.parent = null;
                        StartCoroutine(GoToPoint(initialPos, 1, initialRot));
                    }
                }
                if (drag && !InputManager.StartTouch)
                {
                    drag = false;
                    camera.enabled = true;
                    orbit.enabled = false;
                }
            }
        }
        
    }

    protected IEnumerator GoToPoint(Vector3 position, float transitionTime, bool local = false)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            if (local)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, position, time);
            }else
            {
                transform.position = Vector3.Lerp(transform.position, position, time);
            }
            yield return null;
        }
    }

    protected IEnumerator GoToPoint(Vector3 position, float transitionTime, Quaternion rotation, bool local = false)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / transitionTime;
            if (local)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, position, time);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, time);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, position, time);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, time);
            }
            yield return null;
        }
    }
}
