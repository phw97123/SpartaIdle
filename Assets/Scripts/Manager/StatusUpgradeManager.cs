using System.Collections.Generic;
using UnityEngine;

public class StatusUpgradeManager : Singleton<StatusUpgradeManager>
{
    private List<StatusUpgradeData> upgradeDatas;

    private void Awake()
    {
    }

    private void Start()
    {
        upgradeDatas = new List<StatusUpgradeData>();
        InitStatusUpgradeData(); 
    }

    public void InitStatusUpgradeData()
    {
        List<StatusUpgradeData> loadData = null;
        if (loadData == null)
        {
            BaseStatusUpgradeSO[] datas = Resources.LoadAll<BaseStatusUpgradeSO>("BaseStatusUpgradeSO");
            foreach (var data in datas)
            {
                StatusUpgradeData upgradeData = new StatusUpgradeData(data); 
                upgradeDatas.Add(upgradeData); 
                                               //π⁄»Ò≈ı πŸ∫∏
            }
        }   
    }

    public List<StatusUpgradeData> GetUpgradeDatas()
    {
        return upgradeDatas; 
    }
}
