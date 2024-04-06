using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;
[System.Serializable]
public class IconsAnimation
{
    public string animationNameIdentfier;
    public Sprite iconTexture;
}
public class DetailPanelAnimal : MonoBehaviour
{
    [Header("UI Icon Default")]
    public List<IconsAnimation> IconAnimation;
    [Header("UI Content Detail Element")]
    public Image imgDefault;
    public GameObject PrefabButtonInteract;
    public List<Image> iconStart;
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
        GameObject objAnimalFood = configCage.InstancePrefab(configCage.SoAnimal.PrefabAnimal, configCage.view_Foods);
     
        objAnimalFood.transform.localScale = new Vector3(5, 5, 5);
        foreach (var item in configCage.GetSOfoods())
        {
            GameObject btnInteract = Instantiate(PrefabButtonInteract, detailFoods.transform);
            GameObject objFruit = configCage.InstancePrefab(item.prefab, configCage.view_Foods);
            //set data to button
            btnInteract.GetComponent<Image>().sprite = item.iconFood;
            btnInteract.GetComponent<Button>().onClick.AddListener(() => { configCage.setSoundToAudio(item.voice); });
        }
        //init element charactic
        GameObject objAnimal = configCage.InstancePrefab(configCage.SoAnimal.PrefabAnimal, configCage.view_Characteristic);
        objAnimal.transform.localScale = new Vector3(5f, 5f, 5f);

        Animator currentAnimator = objAnimal.GetComponent<Animator>();
        AnimatorController animatorController = currentAnimator.runtimeAnimatorController as AnimatorController;
        if (animatorController != null)
        {
            foreach (var parameter in animatorController.parameters)
            {
                Debug.Log(parameter.name);
                foreach (var icon in IconAnimation)
                {
                    if (parameter.name.Contains(icon.animationNameIdentfier))
                    {
                       
                        GameObject btnInteract = Instantiate(PrefabButtonInteract, detailCharactics.transform);

                        //set data to button
                        btnInteract.GetComponent<Image>().sprite = icon.iconTexture;
                        btnInteract.GetComponent<Button>().onClick.AddListener(() => SelectAnimation(parameter.name, currentAnimator));
                    }
                }
            }
        }

        //init start Conservationlevel
        for (int i = 0; i < configCage.SoAnimal.dataConservationlevel.LevelStart; i++)
        {
            iconStart[i].color = new Color(249f / 255f, 8f / 255f, 17f / 255f, 1f);

            GameObject objAnimalConservation = configCage.InstancePrefab(configCage.SoAnimal.PrefabAnimal, configCage.view_Conservation);
            int salcePrefab = configCage.SoAnimal.dataConservationlevel.LevelStart+1-i;
            objAnimalConservation.transform.localScale = new Vector3(salcePrefab, salcePrefab, salcePrefab);
        }
    }
    private void ResetUI()
    {
        //Delete all prefab animal spawn in mini environment
        configCage.ResetChildObjectView();
        //Delete all prefab food
        DelAllChildObject(detailFoods.transform);
        //Delete all prefab charactic
        DelAllChildObject(detailCharactics.transform);
        //Reset Icon Start in conservation
        foreach (var icon in iconStart) icon.color = new Color(1f,1f,1f,1f);

        CloseAllDetailPanel();
        detailDefault.SetActive(true);
    }
    private void DelAllChildObject(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject childObject = parent.GetChild(i).gameObject;
            Destroy(childObject);
        }
    }
    void SelectAnimation(string name, Animator animator)
    {
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

        if (animatorController != null)
        {
            foreach (var param in animatorController.parameters)
            {
                animator.SetBool(param.name, false);
            }

            animator.SetBool(name, true);
        }
    }
    #region 5 PANEL BOTTOM
    public void OpenPanelEnvironment()
    {
        CloseAllDetailPanel();
        configCage.SwitchToViewEnvironment();
        StartCoroutine(Common.delayCoroutine(0.5f, () =>
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
        StartCoroutine(Common.delayCoroutine(0.5f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Characteristic);

        }));
    }
    public void OpenPanelConservationlevel()
    {
        OpenPanel(detailConservationlevel);
        configCage.SwitchToViewConservation();
        StartCoroutine(Common.delayCoroutine(0.5f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Conservationlevel);

        }));
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
    public void TURN_OFF_DETAIL_PANEL() //Use in button EXIT in detail panels
    {
        ResetUI();
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
        detailFoods.SetActive(false);
    }
}
