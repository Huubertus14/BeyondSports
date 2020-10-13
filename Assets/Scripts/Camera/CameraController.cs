using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private bool freeRoam;
    private Vector3 offSet;
    private Transform orginTransform;


    private float sensitivityX = 2.5f;
    private float sensitivityY = 1f;

    private float minimumX = -360f;
    private float maximumX = 360f;

    private float minimumY = -90f;
    private float maximumY = 90f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private List<float> rotArrayX = new List<float>();
    private float rotAverageX = 0f;

    private List<float> rotArrayY = new List<float>();
    private float rotAverageY = 0f;

    private float frameCounter = 20;

    private float horizontalSpeed = 15f;
    private float verticalSpeed = 5f;

    private void Awake()
    {
        orginTransform = transform;
        freeRoam = false;
        target = null;
        offSet = new Vector3(0, 600, -800);
    }

    public void ResetTransform()
    {
        target = null;
        freeRoam = false;

        transform.position = orginTransform.position;
        transform.rotation = orginTransform.rotation;
    }

    public void SetLookTarget(GameObject newTarget)
    {
        if (target == newTarget) //if double clicked the same object than stop following it and stay
        {
            target = null;
        }
        target = newTarget;
        freeRoam = false;
    }

    public void SetFreeRoam()
    {
        target = null;
        freeRoam = true;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offSet;
            transform.LookAt(target.transform, transform.forward);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
        if (freeRoam)
        {
            FreeRoamControl();
        }
    }

    private void FreeRoamControl()
    {
        CameraMovement();
        if (Input.GetMouseButton(1))
        {
            CameraMouseRotation();
        }
    }

    private void CameraMovement()
    {
        Vector3 newPos = transform.right * Input.GetAxis("Horizontal") * horizontalSpeed;

        newPos += transform.forward * Input.GetAxis("Vertical") * horizontalSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            //move Up
            newPos += Vector3.up * verticalSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            newPos -= Vector3.up * verticalSpeed;
        }

        transform.position += newPos;
    }

    private void CameraMouseRotation()
    {
        rotArrayX.Clear();
        rotArrayY.Clear();

        rotAverageY = 0f;
        rotAverageX = 0f;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;

        rotArrayY.Add(rotationY);
        rotArrayX.Add(rotationX);

        if (rotArrayY.Count >= frameCounter)
        {
            rotArrayY.RemoveAt(0);
        }
        if (rotArrayX.Count >= frameCounter)
        {
            rotArrayX.RemoveAt(0);
        }

        for (int j = 0; j < rotArrayY.Count; j++)
        {
            rotAverageY += rotArrayY[j];
        }
        for (int i = 0; i < rotArrayX.Count; i++)
        {
            rotAverageX += rotArrayX[i];
        }

        rotAverageY /= rotArrayY.Count;
        rotAverageX /= rotArrayX.Count;

        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        transform.rotation = orginTransform.rotation * xQuaternion * yQuaternion;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
