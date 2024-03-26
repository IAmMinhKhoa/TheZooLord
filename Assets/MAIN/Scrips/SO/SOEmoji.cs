using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Emoji")]
public class SOEmoji : ScriptableObject
{
    public string nameEmoji;
    public List<GameObject>  Prefabs;

    public GameObject GetPrefabEmoji()
    {
        if (Prefabs.Count > 2)
        {
            int randomIndex = Random.Range(0, Prefabs.Count);
            return Prefabs[randomIndex];
        }
        else if (Prefabs.Count == 1)
        {
            return Prefabs[0];
        }
        else
        {
            Debug.LogError("No prefabs available.");
            return null;
        }
    }
}

