using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataGame")]
public class SOGame : ScriptableObject
{
    [SerializeField]private int Coin;

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
