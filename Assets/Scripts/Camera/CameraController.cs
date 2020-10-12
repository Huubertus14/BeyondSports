using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject target;
    private bool freeRoam;
    private Vector3 offSet;

    private Transform orginTransform;
 
    private void Awake()
    {
        freeRoam = false;
        target = null;
        offSet = new Vector3(0,400,-400);
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
        target = newTarget;
        freeRoam = false;
    }

    public void SetFreeRoam(bool value)
    {
        target = null;
        freeRoam = value;
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
        
    }
}
