using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIController : MonoBehaviour
{
    [Header("Inspector Refs:")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject cameraSettingsPanel;

    private bool focusToggle;

    private void Awake()
    {
        cameraSettingsPanel.SetActive(false);
        focusToggle = false;
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

    private void ClickedPlayer(TrackedObjectBahviour clickedPlayer)
    {
        //Set camera focus
        cameraController.SetLookTarget(clickedPlayer.gameObject);
    }
}
