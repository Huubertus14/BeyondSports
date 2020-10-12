using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
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

        teamName = splitData[0];
        trackingID = splitData[1];
        playerNumber = int.Parse(splitData[2]);
        position.x = float.Parse(splitData[3]);
        position.y = float.Parse(splitData[4]);
        speed = float.Parse(splitData[5]);
        
    }

    public string TeamName => teamName;
    public string TrackingId => trackingID;
    public int PlayerNumber => playerNumber;
    public Vector2 Position => position;
    public float Speed => speed;
}
