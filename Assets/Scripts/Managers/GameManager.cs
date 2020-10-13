using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("Editor Refs:")]
    [SerializeField] private MatchVisualizer matchVisualizer;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SimulationController simulationController;

    public MatchVisualizer GetMatchVisualizer => matchVisualizer;
    public CameraController GetCameraController=> cameraController;
    public SimulationController GetSimulationController => simulationController;

}
