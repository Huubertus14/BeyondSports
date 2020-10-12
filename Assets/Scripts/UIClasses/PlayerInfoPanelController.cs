using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoPanelController : MonoBehaviour
{
    [Header("Editor Refs:")]
    [SerializeField] private GameObject infoPanel;
    [Space]
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        TogglePanel(false);
    }

    public void TogglePanel(bool value)
    {
        infoPanel.SetActive(value);

        //Do extra update here
    }

    public void SetData(string newText)
    {
        descriptionText.text = newText;
    }
}
