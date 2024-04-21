using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Aimal")]
public class SOAnimal : ScriptableObject
{
    public bool IsLock=true;
    public string nameAnimal;
    public Sprite defaultImage; //image about this type animal, use in main detail information animal
    public Sprite icon;
    public AudioClip voiceChirp;
    public GameObject PrefabAnimal;
    //----DATA----
    public DataEnvironment dataEnvironment;
    public DataFoods dataFoods;
    public DataCharacteristic dataCharacteristic;
    public DataConservationlevel dataConservationlevel;
    public DataStorySpecial dataStorySpecial;
}
[System.Serializable]
public class DataEnvironment{
    public AudioClip voice;
}

[System.Serializable]
public class DataFoods
{
    public List<SOFood> SoFoods;
}
[System.Serializable]
public class DataCharacteristic
{
    public AudioClip voice;
}

[System.Serializable]
public class DataConservationlevel
{
    public int LevelStart;
    public AudioClip voice;
}
[System.Serializable]
public class DataStorySpecial
{
    public Sprite imgStory;
    public AudioClip voice;
}

//----------

