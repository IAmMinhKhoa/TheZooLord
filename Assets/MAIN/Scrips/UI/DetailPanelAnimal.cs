using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class DetailPanelAnimal : MonoBehaviour
{
    [Header("Button Action")]
    public Button btnExit;
    public Button btnDefault;
    public Button btnEnvironment;
    public Button btnFoods;
    public Button btnConservation;
    [Header("Detail Panel")]
    public GameObject detailDefault;
    public GameObject detailCharactics;
    public GameObject detailConservationlevel;


    public void OpenPanelCharactics()
    {
        OpenPanel(detailCharactics);
    }
    public void OpenPanelConservationlevel()
    {
        OpenPanel(detailConservationlevel);
    }
    public void OpenPanelDefaul()
    {
        OpenPanel(detailDefault);
    }
    private void OpenPanel(GameObject panel)
    {
        CloseAllDetailPanel();
        panel.SetActive(true);
    }
    public void TURN_OFF_DETAIL_PANEL()
    {
        gameObject.SetActive(false);
        this.Broadcast(EventID.OpenUiOverlay);
        this.Broadcast(EventID.OpenInteractCage);
    }
    public void CloseAllDetailPanel()
    {
        detailDefault.SetActive(false);
        detailCharactics.SetActive(false);
        detailConservationlevel.SetActive(false);
    }
}
