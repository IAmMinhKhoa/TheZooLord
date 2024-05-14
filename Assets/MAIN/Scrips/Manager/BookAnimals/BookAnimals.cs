using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookAnimals : MonoBehaviour
{
    public List<SOAnimal> sOAnimals = new List<SOAnimal>();
    private List<SOAnimal> SO_ForestAnimals = new List<SOAnimal>(); 
    private List<SOAnimal> SO_MeadowAnimals = new List<SOAnimal>();
    public InformationBook DetailPrefab;
    public GameObject parentListForest;
    public GameObject parentListMeadow;


    public VerticalLayoutGroup _listLayout = null;
    public ContentFitterRefresh _listSizeRefresher = null;

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
        foreach (var item in SO_ForestAnimals)
        {
            GameObject detail = Instantiate(DetailPrefab.gameObject, parentListForest.transform);
            detail.transform.SetParent(parentListForest.transform, false);
            InformationBook inforDetail = detail.GetComponent< InformationBook > ();
            inforDetail.icon.sprite = item.icon;
            inforDetail.textName.text = item.name;
            inforDetail.dataAnimal = item;
            inforDetail.OnSelect += SelectDetail;
        }
    }

    public void SelectDetail(SOAnimal so)
    {
        Debug.Log("con cac" + so.name); 
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

    }
}


