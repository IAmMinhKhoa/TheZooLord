using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BookAnimals : MonoBehaviour
{
    public List<SOAnimal> sOAnimals = new List<SOAnimal>();
    private List<SOAnimal> SO_ForestAnimals = new List<SOAnimal>(); 
    private List<SOAnimal> SO_MeadowAnimals = new List<SOAnimal>();
    public InformationBook DetailPrefab;
    public GameObject parentListForest;
    public Slider sliderForest;
    public TMP_Text textCountF;
    public TMP_Text textCountM;
    public Slider sliderMeadow;
    public GameObject parentListMeadow;

    //---
    public Transform parentObject3D;
    public VerticalLayoutGroup _listLayout = null;
    public ContentFitterRefresh _listSizeRefresher = null;
    //--
    public GameObject objMainBook;

    private void Awake()
    {
        foreach (var so in sOAnimals)
        {
            if (so.species == SpeciesAnimal.Forset)
            {
                SO_ForestAnimals.Add(so);
            }
            if (so.species == SpeciesAnimal.Meadow)
            {
                SO_MeadowAnimals.Add(so);
            }
        }
        initData();
    }

    public void initData()
    {
        int countActiveF=0, countActiveM = 0;
        foreach (var item in SO_ForestAnimals)
        {
            
            GameObject detail = Instantiate(DetailPrefab.gameObject, parentListForest.transform);
            detail.transform.SetParent(parentListForest.transform, false);
            InformationBook inforDetail = detail.GetComponent< InformationBook > ();
            inforDetail.icon.sprite = item.icon;
            inforDetail.textName.text = item.name;
            inforDetail.dataAnimal = item;
            inforDetail.OnSelect += SelectDetail;
            if (item.IsLock) inforDetail.objLock.SetActive(true);
            if (!item.IsLock) countActiveF++;
        }
        foreach (var item in SO_MeadowAnimals)
        {
            GameObject detail = Instantiate(DetailPrefab.gameObject, parentListMeadow.transform);
            detail.transform.SetParent(parentListMeadow.transform, false);
            InformationBook inforDetail = detail.GetComponent<InformationBook>();
            inforDetail.icon.sprite = item.icon;
            inforDetail.textName.text = item.name;
            inforDetail.dataAnimal = item;
            inforDetail.OnSelect += SelectDetail;
            if (item.IsLock) inforDetail.objLock.SetActive(true);
            if (!item.IsLock) countActiveM++;
        }
        sliderForest.value= (float)countActiveF/ SO_ForestAnimals.Count;
     //   sliderMeadow.value = countActiveM / SO_MeadowAnimals.Count;

        textCountF.text =( countActiveF.ToString() + "/" + SO_ForestAnimals.Count.ToString());
        textCountM.text = (countActiveM.ToString() + "/" + SO_MeadowAnimals.Count.ToString());
    }

    public void SelectDetail(SOAnimal so)
    {
        Debug.Log("con cac" + so.name);
        DelAllChildObject(parentObject3D);
        var animalPrefab = so.PrefabAnimal;
        DestroyImmediate(animalPrefab.GetComponent<NavMeshAgent>(), true);
        DestroyImmediate(animalPrefab.GetComponent<AnimalController>(), true);
        DestroyImmediate(animalPrefab.GetComponent<Rigidbody>(), true);
        DestroyImmediate(animalPrefab.GetComponent<Collider>(), true);
        DestroyImmediate(animalPrefab.GetComponent<ConfigAnimal>(), true);

        Instantiate(animalPrefab, parentObject3D) ;
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

    bool toggleF, toggleM = true;
    public void ToggleForest()
    {
        toggleF = !toggleF;
        parentListForest.SetActive(toggleF);
        _listSizeRefresher.RefreshContentFitters();
    }
    public void ToggleMeadow()
    {
        toggleM = !toggleM;
        parentListMeadow.SetActive(toggleM);
        _listSizeRefresher.RefreshContentFitters();
    }
    public void toggleBook(bool status)
    {
        objMainBook.SetActive(status);
    }
}


