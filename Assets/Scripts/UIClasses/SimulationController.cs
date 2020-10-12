using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulationController : MonoBehaviour
{
    [Header("Editor references")]
    [SerializeField] private TextMeshProUGUI frameCounter;

    public void SetSimulationSpeed(int direction)
    {
        MatchVisualizer.SP.SetDirection(direction);
    }

    private void Update()
    {
        frameCounter.text = "FrameCounter: " + MatchData.SP.GetFrame.GetFrameCount.ToString();
    }

}
