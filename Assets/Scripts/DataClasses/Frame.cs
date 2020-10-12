using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Frame
{
    [SerializeField] private int frameCount;
    [SerializeField] private TrackedObjectData[] trackedObjects;
    [SerializeField] private BallData ballData;

    public Frame(string stringData)
    {
        //Create a frame from a given string
        //Get frame index
        string[] indexString = stringData.Split(':');
        string[] trackedObjectsString = indexString[1].Split(';');
        string ballDataString = indexString[2];

        this.frameCount = int.Parse(indexString[0]);
        trackedObjects = new TrackedObjectData[trackedObjectsString.Length];
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (!string.IsNullOrEmpty(trackedObjectsString[i]))
            {
                trackedObjects[i] = new TrackedObjectData(trackedObjectsString[i]);
            }
        }
        ballData = new BallData(ballDataString);
    }

    public Frame(int frameCount, TrackedObjectData[] trackedObjects, BallData ballData)
    {
        this.frameCount = frameCount;
        this.trackedObjects = trackedObjects;
        this.ballData = ballData;
    }

    public int GetFrameCount => frameCount;
    public BallData GetBallData => ballData;
    public TrackedObjectData[] GetTrackedObjects => trackedObjects;

}
