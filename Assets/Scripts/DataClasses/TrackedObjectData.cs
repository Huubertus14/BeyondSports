using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrackedObjectData
{
    private object[] trackedObjectData;

    [SerializeField] private string teamName;
    [SerializeField] private string trackingID;
    [SerializeField] private int playerNumber;
    [SerializeField] private Vector2 position;
    [SerializeField] private float speed;
    private object[] otherInfo;

    public TrackedObjectData(string team, string trackingID, int playerNumber, Vector2 position, float speed, object[] otherInfo = null)
    {
        this.teamName = team;
        this.trackingID = trackingID;
        this.playerNumber = playerNumber;
        this.position = position;
        this.speed = speed;
        this.otherInfo = otherInfo;
    }

    public TrackedObjectData(string objectData)
    {
        trackedObjectData = objectData.Split(',');

        teamName = trackedObjectData[0].ToString();
        trackingID = trackedObjectData[1].ToString();
        playerNumber = int.Parse(trackedObjectData[2].ToString());
        position.x = float.Parse(trackedObjectData[3].ToString());
        position.y = float.Parse(trackedObjectData[4].ToString());
        speed = float.Parse(trackedObjectData[5].ToString());

        //find not used indexes
        int notUsedIndexes = trackedObjectData.Length - 6; //there are 6 basic values
        otherInfo = new object[notUsedIndexes];

        //Add all optional values here
        for (int i = 0; i < otherInfo.Length; i++)
        {
            otherInfo[i] = trackedObjectData[6 + i];
        }
    }

    public string TeamName => teamName;
    public string TrackingId => trackingID;
    public int PlayerNumber => playerNumber;
    public Vector2 Position => position;
    public float Speed => speed;
    public object GetExtraInfo => otherInfo;
}
