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
    public GameObject PrefabButtonInteract; 
    //Group UI charactics
    [Space(10)]
    [Header("Config Cage")]
    public ConfigCage configCage;
    [Space(10)]
    [Header("Detail Panel")]
    public GameObject detailDefault;
    public GameObject detailFoods;
    public GameObject detailCharactics;
    public GameObject detailConservationlevel;

 
    private void OnEnable()
    {
        InitResource();
    }

    private void InitResource() //set Init Resource in Detail Panel
    {

        //init element DEFAULT
        imgDefault.sprite = configCage.SoAnimal.defaultImage;
        //init element FOODS
        foreach (var item in configCage.GetSOfoods())
        {
            GameObject btnInteract = Instantiate(PrefabButtonInteract, detailFoods.transform);

            //set data to button
            btnInteract.GetComponent<Image>().sprite = item.iconFood;
            btnInteract.GetComponent<Button>().onClick.AddListener(() => { configCage.setSoundToAudio(item.voice); });
        }
        //init element charactic
        AnimatorControllerParameter[] parameters =configCage.SoAnimal.PrefabAnimal.GetComponent<Animator>().contro;
        foreach (var item in parameters)
        {
            Debug.Log(item.name);
        }
    }
    private void ResetUI()
    {
        detailDefault.SetActive(true);
        //destroy button child 
        //food
    }

    #region 5 PANEL BOTTOM
    public void OpenPanelEnvironment()
    {
        CloseAllDetailPanel();
        configCage.SwitchToViewEnvironment();
        StartCoroutine(Common.delayCoroutine(1f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Environment);

        }));
    }
    public void OpenPanelFoods()
    {
        OpenPanel(detailFoods);
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
        StartCoroutine(Common.delayCoroutine(0.5f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Chirp);

        }));
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
