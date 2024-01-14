using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    List<EquipmentData> weaponList;
    List<EquipmentData> armorList; 

    private Dictionary<EquipmentType, List<EquipmentData>> equipmentDatas;
    EquipmentType[] equipmentTypes = { EquipmentType.Weapon, EquipmentType.Armor };
    Rarity[] rarities = { Rarity.Common, Rarity.Uncommon, Rarity.Rare, Rarity.Epic, Rarity.Ancient, Rarity.Legendary, Rarity.Mythology };

    private int maxLevel = 4; 

    private void Awake()
    {
        equipmentDatas = new Dictionary<EquipmentType, List<EquipmentData>>();
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
                List<EquipmentData> equipDataList = new List<EquipmentData>();

                foreach (var data in datasSO)
                {
                    EquipmentData equipData = new EquipmentData(data);
                    equipDataList.Add(equipData);
                }
                equipmentDatas.Add(type, equipDataList);
            }
        }
        weaponList = GetEquipmentDatas(EquipmentType.Weapon);
        armorList = GetEquipmentDatas(EquipmentType.Armor); 
    }

    public List<EquipmentData> GetEquipmentDatas(EquipmentType type)
    {
        return equipmentDatas[type];
    }

    public EquipmentData AutoEquip(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Weapon:
                if (weaponList == null) return null;
                List<EquipmentData> sortWeaponList = weaponList.ToList();
                sortWeaponList.Sort((a, b) => (a.equippedEffect).CompareTo(b.equippedEffect));
                return sortWeaponList[sortWeaponList.Count - 1];
            case EquipmentType.Armor:
                if (armorList == null) return null;
                List<EquipmentData> sortArmorList = armorList.ToList();
                sortArmorList.Sort((a, b) => (a.equippedEffect).CompareTo(b.equippedEffect));
                return sortArmorList[sortArmorList.Count - 1];
        }
        return null; 
    }

    public int Composite(EquipmentData equipment)
    {
        if (equipment.quantity < 4) return -1;
        if ((int)equipment.baseSO.Rarity == rarities.Length - 1 && equipment.level == maxLevel) return -1;

        int compositeCount = equipment.quantity / 4;
        equipment.quantity %= 4;

        EquipmentData nextEquipment = GetNextEquipment(equipment);
        nextEquipment.quantity += compositeCount;
        return compositeCount;
    }

    private EquipmentData GetNextEquipment(EquipmentData equipment)
    {
        List<EquipmentData> datas = GetEquipmentDatas(equipment.baseSO.EquipmentType);

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i] == equipment) 
                return datas[i+1];
        }

        return null; 
    }

    public void AllComposite(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                for (int i = 0; i < weaponList.Count; i++)
                {
                    Composite(weaponList[i]);
                }
                break;
            case EquipmentType.Armor:
                for (int i = 0; i < armorList.Count; i++)
                {
                    Composite(armorList[i]);
                }
                break;
        }
    }
}
