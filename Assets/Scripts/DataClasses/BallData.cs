using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BallData
{
    [SerializeField] private Vector3 ballPosition;
    [SerializeField] private float ballSpeed;
    [SerializeField] private object clickerFlags;

    public BallData(Vector3 position, float speed, object clickerFlags)
    {
        ballPosition = position;
        ballSpeed = speed;
        this.clickerFlags = clickerFlags;
    }

    public BallData(string ballData)
    {
        string[] splitData = ballData.Split(',');
        ballPosition.x = float.Parse(splitData[0]);
        ballPosition.y = float.Parse(splitData[1]);
        ballSpeed = float.Parse(splitData[2]);
    }
}
