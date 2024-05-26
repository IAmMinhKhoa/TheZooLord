using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Question")]
public class SOQuestion : ScriptableObject
{
    public string scripQuestion;
    public AudioClip voiceQuest;
    public List<DataAnswer> dataAnswers;
    [Range(1, 3)]
    public int typeDiff;
    public int reward;
}
[System.Serializable]
public class DataAnswer
{
    public Sprite img;
    public bool rightAnswer;
}
