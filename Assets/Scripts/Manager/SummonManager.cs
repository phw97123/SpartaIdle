using UnityEngine;

public class SummonManager : Singleton<SummonManager>
{
    private SummonType[] summonTypes = { SummonType.Weapon, SummonType.Armor, SummonType.Skill };
    private SummonData[] summonDatas;  

    private void Awake()
    {
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
}
