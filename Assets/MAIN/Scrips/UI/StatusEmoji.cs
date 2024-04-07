using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEmoji : MonoBehaviour
{
    public List<SOEmoji> SO_Emojis = new List<SOEmoji>();
    public Transform parenPrefab;

    public void spawnEmoji(string nameEmoji,float scalseObj=0)
    {
        foreach (var obj in SO_Emojis)
        {
            if(obj.nameEmoji== nameEmoji)
            {
                GameObject emojiPrefab = Instantiate(obj.GetPrefabEmoji(), parenPrefab);
                if(scalseObj!=0) emojiPrefab.transform.localScale = new Vector3(scalseObj, scalseObj, scalseObj);
            }
        }
    }
}
