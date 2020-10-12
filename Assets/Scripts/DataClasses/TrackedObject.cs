using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrackedObject
{
    [SerializeField] private string teamName;
    [SerializeField] private string trackingID;
    [SerializeField] private int playerNumber;
    [SerializeField] private Vector2 position;
    [SerializeField] private float speed;

    public TrackedObject(string team, string trackingID, int playerNumber, Vector2 position, float speed)
    {
        this.teamName = team;
        this.trackingID = trackingID;
        this.playerNumber = playerNumber;
        this.position = position;
        this.speed = speed;
    }

    public TrackedObject(string objectData)
    {
        string[] splitData = objectData.Split(',');
        
        teamName = splitData[0];
        trackingID = splitData[1];
        playerNumber = int.Parse(splitData[2]);
        position.x = float.Parse(splitData[3]);
        position.y = float.Parse(splitData[4]);
        speed = float.Parse(splitData[5]);
    }
}
