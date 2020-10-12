using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Frame
{
    [SerializeField] private int frameCount;
    [SerializeField] private TrackedObject[] trackedObjects;
    [SerializeField] private BallData ballData;

    public Frame(string stringData)
    {
        //Create a frame from a given string
        //Get frame index
        string[] indexString = stringData.Split(':');
        string[] trackedObjectsString = indexString[1].Split(';');
        string ballDataString = indexString[2];

        this.frameCount = int.Parse(indexString[0]);
        trackedObjects = new TrackedObject[trackedObjectsString.Length];
        for (int i = 0; i < trackedObjects.Length; i++)
        {
            if (!string.IsNullOrEmpty(trackedObjectsString[i]))
            {
                trackedObjects[i] = new TrackedObject(trackedObjectsString[i]);
            }
        }
        ballData = new BallData(ballDataString);
    }

    public Frame(int frameCount, TrackedObject[] trackedObjects, BallData ballData)
    {
        this.frameCount = frameCount;
        this.trackedObjects = trackedObjects;
        this.ballData = ballData;
    }


}
