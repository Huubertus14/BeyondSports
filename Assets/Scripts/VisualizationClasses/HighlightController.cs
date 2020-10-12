using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [Header("Inspector refs:")]
    [SerializeField] private PlayerInfoPanelController infoController;


    private Camera camera;
    private RaycastHit hit;

    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                IHighlightObject highlight = hit.transform.gameObject.GetComponent<IHighlightObject>();
                if (highlight != null)
                {
                    //Set panel info
                    infoController.TogglePanel(true);
                    infoController.SetData(highlight.Description());
                }
                else
                {
                    infoController.TogglePanel(false);
                }
            }
        }
    }

}
