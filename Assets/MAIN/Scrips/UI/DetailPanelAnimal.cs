using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class DetailPanelAnimal : MonoBehaviour
{
    [Header("UI Content Detail Element")]
    public Image imgDefault;
    public Image imgConservationlevel;
    //Group UI charactics
    [Space(10)]
    [Header("Config Cage")]
    public ConfigCage configCage;
    [Space(10)]
    [Header("Detail Panel")]
    public GameObject detailDefault;
    public GameObject detailCharactics;
    public GameObject detailConservationlevel;



    private void InitResource()
    {

    }
    private void ResetUI()
    {

    }

    #region 5 PANEL BOTTOM
    public void OpenPanelEnvironment()
    {
        CloseAllDetailPanel();
        configCage.SwitchToViewEnvironment();
    }
    public void OpenPanelFoods()
    {
        CloseAllDetailPanel();
        configCage.SwitchToViewFoods();
    }
    public void OpenPanelCharactics()
    {
        OpenPanel(detailCharactics);
        configCage.SwitchToViewCharacteristic();
    }
    public void OpenPanelConservationlevel()
    {
        OpenPanel(detailConservationlevel);
    }

    public void OpenPanelDefaul()
    {
        OpenPanel(detailDefault);
    }
    #endregion

    private void OpenPanel(GameObject panel)
    {
        CloseAllDetailPanel();
        panel.SetActive(true);
    }
    public void TURN_OFF_DETAIL_PANEL()
    {
        OpenPanelDefaul();
        gameObject.SetActive(false);
        configCage.cameraCage.gameObject.SetActive(false);
        this.Broadcast(EventID.OpenUiOverlay);
        this.Broadcast(EventID.OpenInteractCage);
    }
    private void CloseAllDetailPanel()
    {
        detailDefault.SetActive(false);
        detailCharactics.SetActive(false);
        detailConservationlevel.SetActive(false);
    }
}
