using System;
using UnityEngine;

[Serializable]
public struct ProbabilityData
{
    public int[] probabilityArray; 
}

[CreateAssetMenu()]
public class SummonSO : ScriptableObject
{
    [SerializeField] ProbabilityData[] probabilities; 
    public int[] Getprobalility(int level)
    {
        return probabilities[level-1].probabilityArray;
    }
}
