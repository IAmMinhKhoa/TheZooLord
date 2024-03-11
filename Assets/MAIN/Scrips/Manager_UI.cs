using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_UI : MonoBehaviour
{
    public GameObject groupButtonFood;

    private void Start()
    {

        PointInteract.OnEnterTrigger += PointInteract_OnEnterTrigger;
        PointInteract.OnExitTrigger += PointInteract_OnExitTrigger;
    }

    private void PointInteract_OnExitTrigger()
    {
        groupButtonFood.SetActive(false);
    }

    private void PointInteract_OnEnterTrigger()
    {
        groupButtonFood.SetActive(true);
    }
}
