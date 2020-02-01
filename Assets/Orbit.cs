using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    [Header("Orbital Option")]
    [SerializeField]
    protected float xSpeed = 20.0f;
    [SerializeField]
    protected float ySpeed = 20.0f;
    [SerializeField]
    protected float maxspeed = 5f;
    [SerializeField]
    protected float yMinLimit = -90f;
    [SerializeField]
    protected float yMaxLimit = 90f;
    [SerializeField]
    protected float distanceMin = 10f;
    [SerializeField]
    protected float distanceMax = 10f;
    [SerializeField]
    protected float decelerationSpeed = 2f;

    protected float distance = 2.0f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    void LateUpdate()
    {
        if (InputManager.Drag)
        {
            velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
            velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
        }

        velocityX = Mathf.Clamp(velocityX, -maxspeed, maxspeed);
        velocityY = Mathf.Clamp(velocityY, -maxspeed, maxspeed);
        rotationYAxis += velocityX;
        rotationXAxis -= velocityY;
        Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        Quaternion rotation = toRotation;
        transform.rotation = rotation;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * decelerationSpeed);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * decelerationSpeed);
    }
}
