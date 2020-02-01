using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool click;
    private Vector3 beginMousePosition;
    private bool startTouch;
    private bool drag;
    [SerializeField]
    protected float deathDistance;
    static public bool Drag;
    static public bool Click;
    static public bool StartTouch;
    void Update()
    {
        click = false;
        if (Input.GetMouseButtonUp(0))
        {
            if (!drag)
            {
                click = true;
            }
            startTouch = false;
            drag = false;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(0))
        {
            if (!startTouch)
            {
                beginMousePosition = Input.mousePosition;
                startTouch = true;
            }
            else
            {
                float distance = Vector3.Distance(beginMousePosition, Input.mousePosition);
                if (distance >= deathDistance)
                {
                    drag = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }

            }
        }
        Drag = drag;
        Click = click;
        StartTouch = startTouch;
    }
}
