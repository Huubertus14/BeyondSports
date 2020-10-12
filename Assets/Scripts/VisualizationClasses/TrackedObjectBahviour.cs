using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackedObjectBahviour : MonoBehaviour
{
    private Renderer objectRenderer;

    private string team;
    private string trackingID;
    private int playerNumber;

    [Header("Editor Refs")]
    [SerializeField] private TextMeshPro playerNumberText;

    public TrackedObjectData objectData;

    public void SetInitValues(TrackedObjectData data)
    {
        objectRenderer = GetComponent<Renderer>();

        objectData = data;
        team = data.TeamName;
        trackingID = data.TrackingId;
        playerNumber = data.PlayerNumber;
        transform.position = CalculatePosition(data);

        playerNumberText.text = playerNumber.ToString();

        SetPlayerColor(team);
    }

    private void SetPlayerColor(string team) //not good
    {
        //Get Color from team number
        switch (team)
        {
            case "0":
                objectRenderer.material.color = Color.red;
                break;
            case "1":
                objectRenderer.material.color = Color.blue;
                break;
            case "2":
                objectRenderer.material.color = Color.black;
                break;
            default:
                objectRenderer.material.color = Color.white;
                break;
        }
    }

    public void SetPosition(TrackedObjectData data)
    {
        transform.position = CalculatePosition(data);
    }

    private Vector3 CalculatePosition(TrackedObjectData data)
    {
        return new Vector3(data.Position.x, 2, data.Position.y);
    }
}
