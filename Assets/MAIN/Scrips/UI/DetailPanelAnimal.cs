using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;
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
    public VideoPlayer videoSpecialStory;
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
    public GameObject detailStorySpecial;

    //---Scale obejct---
    float _ScaleAnimalInFood = 8f;
    float _ScaleAnimalInCharactic = 8f;
    float _ScaleFood = 7f;

    private void OnEnable()
    {        
        InitResource();
    }

    private void InitResource() //set Init Resource in Detail Panel
    {
        // 1. Meaningful Variable Names and Comments
        var defaultImage = configCage.SoAnimal.defaultImage; // Use descriptive names
        var animalPrefab = configCage.SoAnimal.PrefabAnimal;
        var animalFoods = configCage.GetSOfoods();
        var animalClip = configCage.SoAnimal.dataStorySpecial.clipAnimalSpecial;
        Debug.Log("khoa :" + animalPrefab);
        BoxCollider coll = animalPrefab.GetComponent<BoxCollider>();
        coll.isTrigger = false;
        coll.size = new Vector3(1f, 1f, 1f);
        Rigidbody rigid= animalPrefab.GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.useGravity = true;
        DestroyImmediate(animalPrefab.GetComponent<NavMeshAgent>(),true);
        DestroyImmediate(animalPrefab.GetComponent<AnimalController>(),true);

        // 3. Element Initialization (DEFAULT, FOODS)
        imgDefault.sprite = defaultImage;

        GameObject foodObject = configCage.InstancePrefab(animalPrefab, configCage.view_Foods); // Combine for readability
        foodObject.GetComponent<ConfigAnimal>().spawnEmoji("Hungry");
        foodObject.transform.localScale = new Vector3(_ScaleAnimalInFood, _ScaleAnimalInFood, _ScaleAnimalInFood);
        int _indexStorage = 0;
        foreach (var foodItem in animalFoods)
        {
            GameObject button = Instantiate(PrefabButtonInteract, detailFoods.transform);
            GameObject fruitObject = configCage.InstancePrefab(foodItem.prefab, configCage.view_Foods);
           // fruitObject.transform.localScale = new Vector3(_ScaleFood, _ScaleFood, _ScaleFood);
            Debug.Log("khoa count" + _indexStorage+"/"+ configCage.view_Storage.Length);
            Debug.Log("khoa:" + configCage.view_Storage[_indexStorage]);
            fruitObject.transform.position = configCage.view_Storage[_indexStorage].position;

            button.GetComponent<Image>().sprite = foodItem.iconFood;
            //button.GetComponent<Button>().onClick.AddListener(() => SoundManager.instance.PlayAudioSingle(foodItem.voice));
            _indexStorage++;
        }

        // 4. Element Initialization (CHARACTERISTIC)
        GameObject animalObject = configCage.InstancePrefab(animalPrefab, configCage.view_Characteristic);
        animalObject.transform.localScale = new Vector3(_ScaleAnimalInCharactic, _ScaleAnimalInCharactic, _ScaleAnimalInCharactic);

        Animator animator = animalObject.GetComponent<Animator>();
        if (animator != null)
        {
            RuntimeAnimatorController controller = animator.runtimeAnimatorController as RuntimeAnimatorController;
            if (controller != null)
            {
                foreach (var parameter in controller.animationClips)
                {
                    foreach (var icon in IconAnimation)
                    {
                        if (parameter.name.Contains(icon.animationNameIdentfier))
                        {
                            GameObject button = Instantiate(PrefabButtonInteract, detailCharactics.transform);
                            button.GetComponent<Image>().sprite = icon.iconTexture;
                            button.GetComponent<Button>().onClick.AddListener(() => SelectAnimation(parameter.name, animator));
                        }
                    }
                }
            }
        }

        // 5. Element Initialization (START CONSERVATION LEVEL)
        float ScaleSpawn = 7f;
        float tempPositionZ = configCage.view_Conservation.transform.position.z;
        for (int i = 1; i <= configCage.SoAnimal.dataConservationlevel.LevelStart; i++)
        {
            iconStart[i-1].color = new Color(249f / 255f, 8f / 255f, 17f / 255f, 1f);
            
            GameObject conservationObject = configCage.InstancePrefab(animalPrefab, configCage.view_Conservation);
           
              
            conservationObject.transform.localScale = new Vector3(ScaleSpawn, ScaleSpawn, ScaleSpawn);
            conservationObject.transform.position = new Vector3(configCage.view_Conservation.transform.position.x, configCage.view_Conservation.transform.position.y, tempPositionZ );
            ScaleSpawn -= 0.7f;
            tempPositionZ += 5f;
            if (configCage.SoAnimal.dataConservationlevel.LevelStart >= 3)
            {
                conservationObject.GetComponent<ConfigAnimal>().spawnEmoji("Eat");
            }
            else
            {
                conservationObject.GetComponent<ConfigAnimal>().spawnEmoji("Sad");
            }
        }
        //6. sotry special of animal
        videoSpecialStory.clip = animalClip;
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
        RuntimeAnimatorController animatorController = animator.runtimeAnimatorController as RuntimeAnimatorController;

        if (animatorController != null)
        {
            foreach (var param in animatorController.animationClips)
            {
                Debug.Log("khoa ngu:" + param.name+ animatorController.animationClips);
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
        StartCoroutine(Common.delayCoroutine(0.2f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Environment);

        }));
    }
    public void OpenPanelFoods()
    {
        OpenPanel(detailFoods);
        configCage.SwitchToViewFoods();
        StartCoroutine(Common.delayCoroutine(0.2f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Food);

        }));
    }
    public void OpenPanelCharactics()
    {
        OpenPanel(detailCharactics);
        configCage.SwitchToViewCharacteristic();
        StartCoroutine(Common.delayCoroutine(0.2f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.Characteristic);

        }));
    }
    public void OpenPanelConservationlevel()
    {
        OpenPanel(detailConservationlevel);
        configCage.SwitchToViewConservation();
        StartCoroutine(Common.delayCoroutine(0.2f, () =>
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
    public void OpenPanelStorySpecial()
    {
        OpenPanel(detailStorySpecial);
        StartCoroutine(Common.delayCoroutine(0.5f, () =>
        {
            configCage.PlaySoundType(SoundTypeInCage.StorySpecial);

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
        Manager_UI.Instance.CloseModalViewDetailAnimal();
        configCage.cameraCage.gameObject.SetActive(false); //turn off cammera of cage -> refund camera follow player
        
    }
    private void CloseAllDetailPanel()
    {
        detailDefault.SetActive(false);
        detailCharactics.SetActive(false);
        detailConservationlevel.SetActive(false);
        detailFoods.SetActive(false);
        detailStorySpecial.SetActive(false);
    }
}
