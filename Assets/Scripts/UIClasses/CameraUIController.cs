using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIController : MonoBehaviour
{
    [Header("Inspector Refs:")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject cameraSettingsPanel;

    private void Awake()
    {
        cameraSettingsPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        cameraSettingsPanel.SetActive(!cameraSettingsPanel.activeSelf);
    }

    public void ResetPosition()
    {
        cameraController.ResetTransform();
    }

    public void ToggleFreeRoam(bool value)
    {

    }

    public void FocusOnBall()
    {
        cameraController.SetLookTarget(MatchVisualizer.SP.GetBall.gameObject);
    }

    public void FocusOnPlayer()
    {
        //Start listening if a player is pressed
        TrackedObjectBahviour.trackedObjectDelegate += ClickedPlayer;
    }

    private void ClickedPlayer(TrackedObjectBahviour clickedPlayer)
    {
        //Set camera focus


    }
}
