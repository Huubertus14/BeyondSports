using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackedObjectBahviour : MonoBehaviour, IHighlightObject
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

        UpdateObject(data);
    }

    private void SetPlayerColor(int team) //Can be cleaner
    {
        //Get Color from team number
        switch (team)
        {
            case 0:
                objectRenderer.material.color = Color.red;
                break;
            case 1:
                objectRenderer.material.color = Color.blue;
                break;
            case 2:
                objectRenderer.material.color = Color.black;
                break;
            case -1:
                gameObject.SetActive(false);
                break;
            default:
                objectRenderer.material.color = Color.white;
                break;
        }
    }

    public void UpdateObject(TrackedObjectData data)
    {
        gameObject.SetActive(true);
        objectData = data;
        team = data.TeamName;
        trackingID = data.TrackingId;
        playerNumber = data.PlayerNumber;
        transform.position = CalculatePosition(data);

        playerNumberText.text = playerNumber.ToString();
        SetPlayerColor(int.Parse(team));
    }

    private Vector3 CalculatePosition(TrackedObjectData data)
    {
        return new Vector3(data.Position.x, 2, data.Position.y);
    }

    public string Description()
    {
        string desc = "Team: " + objectData.TeamName;
        desc += "\n Number: " + objectData.PlayerNumber;
        desc += "\n Position: " + objectData.Position;
        desc += "\n Speed: " + objectData.Speed;
        return desc;
    }

    public void SetHighLight()
    {
        GameManager.SP.GetCameraController.SetLookTarget(gameObject);
    }


    public string TrackID => trackingID;
}
