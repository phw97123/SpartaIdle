using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    private Dictionary<EquipmentType, List<EquipmentData>> equipmentDatas;
    List<EquipmentData> equipDataList;
    EquipmentType[] equipmentTypes = { EquipmentType.Weapon, EquipmentType.Armor };

    private void Awake()
    {
        equipmentDatas  = new Dictionary<EquipmentType, List<EquipmentData>>();
        InitEquipmentData(); 
    }

    public void InitEquipmentData()
    {
        Dictionary<EquipmentType, List<EquipmentData>> loadDatas = null;
        if (loadDatas == null)
        {
            foreach (var type in equipmentTypes)
            {
                BaseEquipmentSO[] datasSO = Resources.LoadAll<BaseEquipmentSO>($"BaseEquipmentSO/{type}");
                equipDataList = new List<EquipmentData>();

                foreach (var data in datasSO)
                {
                    EquipmentData equipData = new EquipmentData(data);
                    equipDataList.Add(equipData);
                }
                equipmentDatas.Add(type, equipDataList);
            }
        }
    }

    public List<EquipmentData> GetEquipmentDatas(EquipmentType type)
    {
        return equipmentDatas[type];
    }
}
