using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationController : MonoBehaviour
{
    [Header("Editor references")]
    [SerializeField] private TextMeshProUGUI frameCounter;

    private Slider frameSlider;

    private void Awake()
    {
        frameSlider = GetComponentInChildren<Slider>();
        frameSlider.gameObject.SetActive(false);
    }

    public void CreateSlider(int minValue, int maxValue, int currentValue)
    {
        frameSlider.gameObject.SetActive(true);
        frameSlider.minValue = minValue;
        frameSlider.maxValue = maxValue;
        frameSlider.value = currentValue;
    }

    public void UpdateSlider(int currentValue)
    {
        if (frameSlider.gameObject.activeSelf)
        {
            frameSlider.value = currentValue;
        }
    }

    public void SliderChanged()
    {
        //Set frame count to the right value
        MatchData.SP.SetIndex((int)frameSlider.value);
    }

    public void SetSimulationSpeed(int direction)
    {
        MatchVisualizer.SP.SetDirection(direction);
    }

    private void Update()
    {
        frameCounter.text = "FrameCounter: "+MatchData.SP.GetIndex;
    }

}
