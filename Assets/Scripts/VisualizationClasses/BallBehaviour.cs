using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{


    public void SetValues(BallData data)
    {
        transform.position = CalculateData(data);
    }

    private Vector3 CalculateData( BallData data)
    {
        return new Vector3(data.Position.x, data.Position.z, data.Position.y);
    }
}
