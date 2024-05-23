using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataGame")]
public class SOGame : ScriptableObject
{
    [SerializeField]private int Coin;
    public float CDTimerPlay;
    public bool CanPlay;
    public inforZoo zooMeadow;

    public void SubtractCoin(int value)
    {
        Coin-=value;
    }
    public void AddCoin(int value)
    {
        Coin += value;
    }
    public int GetCoint() { return Coin; }
}


[System.Serializable]
public class inforZoo{
    public int index=0;
    public int costOpen;
    public bool isActive;
    public string nameZoo;
}
