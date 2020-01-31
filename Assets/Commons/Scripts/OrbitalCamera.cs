
using UnityEngine;
using System.Collections;

public class OrbitalCamera : MonoBehaviour
{
    [Header("LookAt")]
    [SerializeField]
    protected new Transform camera;

    [Header("Orbital Option")]
    [SerializeField]
    protected float xSpeed = 20.0f;
    [SerializeField]
    protected float ySpeed = 20.0f;
    [SerializeField]
    protected float maxspeed;
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
    [Header("zoomOption")]
    [SerializeField]
    protected float cameraPivotDistance = 10f;
    [SerializeField]
    protected float zoomSpeed;

    protected float distance = 2.0f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    void LateUpdate()
    {
        if (camera)
        {
            if (Input.GetMouseButton(0))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }

            velocityX = Mathf.Clamp(velocityX, -maxspeed, maxspeed);
            velocityY = Mathf.Clamp(velocityY, -maxspeed, maxspeed);
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;
            transform.rotation = rotation;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * decelerationSpeed);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * decelerationSpeed);
            CheckZoom();
        }
    }

    public void CheckZoom()
    {
        cameraPivotDistance = Mathf.Clamp(cameraPivotDistance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, distanceMin, distanceMax);
        camera.localPosition = Vector3.Lerp(camera.localPosition,new Vector3(0, 0, -cameraPivotDistance), zoomSpeed * Time.deltaTime);
    }

    private void OnValidate()
    {
        CheckZoom();
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

