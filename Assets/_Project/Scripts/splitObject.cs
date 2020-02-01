using Pixelplacement;
using Pixelplacement.TweenSystem;
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
    private static splitObject inHand;
    protected bool drag;

    protected Vector3 initialPos;
    protected Quaternion initialRot;

    protected Coroutine currentCoroutine;
    protected MeshCollider meshCollider;

    protected TweenBase currentTween;
    protected TweenBase currentTween2;

    public bool IsFixed 
    { 
        get => isFixed;
        set
        {
            isFixed = value;
            CheckFace();
        }
    }

    protected static splitObject InHand 
    { 
        get => inHand;
        set
        {
            inHand = value;
            CheckHand();
        }
    }

    private static void CheckHand()
    {
        foreach(splitObject split in FindObjectsOfType<splitObject>()){
            if(inHand is null)
            {
                if (split.IsFixed)
                {
                    foreach (snapFace snap in split.snapFaces)
                    {
                        snap.GetComponent<Collider>().enabled = false;
                    }
                }
                split.GetComponent<Collider>().enabled = true;
            }
            else if (inHand == split)
            {
                split.GetComponent<Collider>().enabled = true;
            }
            else
            {
                if (split.IsFixed)
                {
                    split.GetComponent<Collider>().enabled = true;

                    foreach (snapFace snap in split.snapFaces)
                    {
                        snap.GetComponent<Collider>().enabled = true;
                        snap.CheckCompatible();
                    }
                }
                else
                {
                    split.GetComponent<Collider>().enabled = false;
                }
            }
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
        initialPos = transform.position;
        initialRot = transform.rotation;
        snapFaces = GetComponentsInChildren<snapFace>().ToList();
        orbit = GetComponent<Orbit>();
        camera = FindObjectOfType<OrbitalCamera>();
        meshCollider = GetComponent<MeshCollider>();
        CheckFace();
        CheckHand();
        
    }

    protected void OnMouseOver()
    {
        if (InputManager.StartTouch)
        {
            if (inHand == this)
            {
                drag = true;
                camera.enabled = false;
                orbit.enabled = true;
            }
        }
        if (InputManager.Click)
        {
            if (inHand == null)
            {
                if (!IsFixed)
                {
                    gameObject.layer = 5;
                    InHand = this;
                    camera.enabled = true;
                    orbit.enabled = false;
                    inHud = true;
                    Transform position = GameObject.FindGameObjectWithTag("CubePosition").transform;
                    transform.parent = position;
                    GoToPoint(Vector3.zero,1, true);
                }
            }
        }
    }

    private void Update()
    {
        if (inHud && InputManager.Click)
        {
            int layer = 1 << 5 | 1 << 2;
            layer = ~layer;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, layer))
            {
                Debug.Log(hit.transform);
                if (hit.transform.GetComponent<snapFace>())
                {
                    
                    if (hit.transform.GetComponent<snapFace>().compatibleObject == gameObject)
                    {
                        inHud = false;
                        InHand = null;
                        gameObject.layer = 9;
                        transform.parent = null;
                        GoToPoint(hit.transform.position, 1, hit.transform.rotation);
                        IsFixed = true;
                        foreach (snapFace face in snapFaces)
                        {
                            face.gameObject.SetActive(true);
                            face.CheckCompatible();
                        }
                    }
                }
                else if (!hit.transform.GetComponent<splitObject>())
                {
                    inHud = false;
                    InHand = null;
                    gameObject.layer = 9;
                    transform.parent = null;
                    GoToPoint(initialPos, 1, false);
                }
            }
        }
        if (drag && !InputManager.StartTouch)
        {
            drag = false;
            camera.enabled = true;
            orbit.enabled = false;
        }

    }

    protected void GoToPoint(Vector3 position, float transitiontime, bool local = false)
    {

        if (currentTween != null)
        {
            currentTween.Stop();
        }
        if (currentTween2 != null)
        {
            currentTween2.Stop();
        }

        if (local)
        {
            currentTween = Tween.LocalPosition(transform, position, transitiontime,0, Tween.EaseInOut);
        } else
        {
            currentTween = Tween.Position(transform, position, transitiontime, 0, Tween.EaseInOut);
        }
    }

    protected void GoToPoint(Vector3 position, float transitiontime, Quaternion rotation, bool local = false)
    {
        if (currentTween != null)
        {
            currentTween.Stop();
        }
        if (currentTween2 != null)
        {
            currentTween2.Stop();
        }

        if (local)
        {
            currentTween = Tween.LocalPosition(transform, position, transitiontime, 0, Tween.EaseInOut);
            currentTween2 = Tween.LocalRotation(transform, rotation, transitiontime, 0, Tween.EaseInOut);
        }
        else
        {
            currentTween = Tween.Position(transform, position, transitiontime, 0, Tween.EaseInOut);
            currentTween2 =Tween.Rotation(transform, rotation, transitiontime, 0, Tween.EaseInOut);

        }
    }
}
