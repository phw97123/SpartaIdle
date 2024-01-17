using System.Collections.Generic;
using UnityEngine;

public class SummonManager : Singleton<SummonManager>
{
    private SummonType[] summonTypes = { SummonType.Weapon, SummonType.Armor, SummonType.Skill };
    private SummonData[] summonDatas;

    private EquipmentManager equipmentManager;

    private void Awake()
    {
        equipmentManager = EquipmentManager.Instance;

        CreateSummonDatas(); 
    }

    private void CreateSummonDatas()
    {
        summonDatas = new SummonData[summonTypes.Length];
        for (int i = 0; i < summonTypes.Length; i++)
        {
            summonDatas[i] = new SummonData(summonTypes[i]); 
        }
    }

    public SummonData[] GetSummonDatats()
    {
        return summonDatas;
    }

    public List<EquipmentData> SummonEquipment(SummonData data, int count)
    {
        int[] probabilityArray = GetEquipmentProbabilities(data.summonLevel);
      
        List<EquipmentData> summonEquipmentDatas = new List<EquipmentData>();
        Debug.Log($"Count : {count}"); 
        for (int i = 0; i < count; i++)
        {
            int randomValue = Random.Range(1, 1001);
            int cumulativeProbability = 0;
            for (int j = 0; j<probabilityArray.Length;j++)
            {
                cumulativeProbability += probabilityArray[j];

                if (randomValue <= cumulativeProbability)
                {
                    int equipmentLevel = Random.Range(1, 5);
                    string equipmentName = $"{j + 1}{equipmentLevel}_{data.type}_{(Rarity)j}";

                    EquipmentData summonedEquipment = equipmentManager.GetEquipment(equipmentName);
                    Debug.Log(summonedEquipment.name); 
                    summonedEquipment.quantity++; 
                    summonEquipmentDatas.Add(summonedEquipment);
                        break;
                }
            }
        }
        return summonEquipmentDatas;
    }

    private int[] GetEquipmentProbabilities(int level)
    {
        SummonSO summonSo = Resources.Load<SummonSO>("SummonProbSO/EquipmentSO");
        int[] probabilities = summonSo.Getprobalility(level);
        return probabilities; 
    }
}
