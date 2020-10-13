using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [Header("Inspector refs:")]
    [SerializeField] private PlayerInfoPanelController infoController;

    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                IHighlightObject highlight = hit.transform.gameObject.GetComponent<IHighlightObject>();
                if (highlight != null)
                {
                    infoController.TogglePanel(true);                   //enable panel
                    infoController.SetData(highlight.Description());    //set right data on the panel
                    highlight.SetHighLight();                           //set the given object as a highlight object
                }
                else
                {
                    infoController.TogglePanel(false);
                }
            }
        }
    }

}
