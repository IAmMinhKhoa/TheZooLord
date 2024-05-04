using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public List<ConfigCage> configCages;

    public Transform view_Environment;
    public Transform view_Foods;
    public Transform view_Characteristic;
    public Transform view_Conservation;
    //--storage food--
    public Transform[] view_Storage;

    private void Awake()
    {
        foreach (var config in configCages)
        {
            config.SetTargetMiniEnvironment(view_Environment, view_Foods, view_Characteristic, view_Conservation, view_Storage);
        }
    }
}
