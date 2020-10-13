using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

[Serializable]
public class TrackedObjectData
{
    private object[] trackedObjectData;

    private string teamName;
    private string trackingID;
    private int playerNumber;
    private Vector2 position;
    private float speed;
    private string firstDown;

    public TrackedObjectData(string team, string trackingID, int playerNumber, Vector2 position, float speed, string firstDown = "")
    {
        this.teamName = team;
        this.trackingID = trackingID;
        this.playerNumber = playerNumber;
        this.position = position;
        this.speed = speed;
        this.firstDown = firstDown;
    }

    public TrackedObjectData(string objectData)
    {
        string[] splitData = objectData.Split(',');

        trackedObjectData = new object[splitData.Length];

        for (int i = 0; i < trackedObjectData.Length; i++)
        {
            trackedObjectData[i] = splitData[i];
        }

        teamName = trackedObjectData[0].ToString();
        trackingID = trackedObjectData[1].ToString();
        playerNumber = (int)trackedObjectData[2];
        position.x = (float)trackedObjectData[3];
        position.y = (float)trackedObjectData[4];
        speed = (float)trackedObjectData[5];

        //Add all optional values here
        try
        {
            firstDown = trackedObjectData[6].ToString();
        }
        catch (Exception)
        {

            throw;
        }

    }

    public string TeamName => teamName;
    public string TrackingId => trackingID;
    public int PlayerNumber => playerNumber;
    public Vector2 Position => position;
    public float Speed => speed;
    public string FirstDown => firstDown;
}
