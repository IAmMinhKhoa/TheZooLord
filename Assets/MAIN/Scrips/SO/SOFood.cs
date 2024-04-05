using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu( menuName = "Food")]
public class SOFood : ScriptableObject
{
    public string nameFood;

    public Sprite iconFood;
    public GameObject prefab;
    public AudioClip voice;
}
