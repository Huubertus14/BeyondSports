using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("Editor Refs:")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SimulationController simulationController;

    public CameraController GetCameraController=> cameraController;
    public SimulationController GetSimulationController => simulationController;
}
