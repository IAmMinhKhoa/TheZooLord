using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Aimal")]
public class SOAnimal : ScriptableObject
{
    public bool clock=true;
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
    public List<TypeAnimation> typeAnimations;
    public AudioClip voice;
}

[System.Serializable]
public class DataConservationlevel
{
    public int LevelStart;
    public AudioClip voice;
}


//----------

[System.Serializable]
public class TypeAnimation
{
    public TypeAnimationAnimal type;
    public string VNTypeAnimation;
}