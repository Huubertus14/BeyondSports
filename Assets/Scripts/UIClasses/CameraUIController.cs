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

    public void ToggleFreeRoam()
    {
        cameraController.SetFreeRoam();
    }

    public void FocusOnBall()
    {
        cameraController.SetLookTarget(GameManager.SP.GetMatchVisualizer.GetBall.gameObject);
    }

    
}
