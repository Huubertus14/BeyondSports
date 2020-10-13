using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour, IHighlightObject
{
    BallData objectData;
    public string Description()
    {
        string desc = "position: " + objectData.Position;
        desc += "\n Speed: " + objectData.Speed;
        return desc;
    }

    public void SetHighLight()
    {

    }

    public void SetValues(BallData data)
    {
        transform.position = CalculateData(data);
    }

    private Vector3 CalculateData(BallData data)
    {
        return new Vector3(data.Position.x, data.Position.z, data.Position.y);
    }
}
