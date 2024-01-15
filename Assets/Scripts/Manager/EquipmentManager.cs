using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    List<EquipmentData> weaponList;
    List<EquipmentData> armorList;

    private Dictionary<EquipmentType, List<EquipmentData>> equipmentDatas;
    EquipmentType[] equipmentTypes = { EquipmentType.Weapon, EquipmentType.Armor };
    Rarity[] rarities = { Rarity.Common, Rarity.Rare, Rarity.Epic, Rarity.Ancient, Rarity.Legendary, Rarity.Mythology };
    Color[] colors = { Color.gray, Color.green,  Color.blue, Color.yellow, Color.magenta, Color.red };

    private int maxLevel = 4;

    private void Awake()
    {
        equipmentDatas = new Dictionary<EquipmentType, List<EquipmentData>>();
        weaponList = new List<EquipmentData>();
        armorList = new List<EquipmentData>();

        SetAllEquipment();
    }

    private void SetAllEquipment()
    {
        CreateAllWeapon();
        CreateAllArmor();
    }

    private void CreateAllWeapon()
    {
        int rarityIntValue = 0;
        int weaponCount = 1;

        foreach (Rarity rarity in rarities)
        {
            if (rarity == Rarity.None) continue;
            rarityIntValue = Convert.ToInt32(rarity);
            for (int level = 1; level <= maxLevel; level++)
            {
                // 11_Weapon_Common
                string name = $"{rarityIntValue}{level}_{EquipmentType.Weapon}_{rarity}";

                int equippedEffect = level * ((int)Mathf.Pow(10, rarityIntValue + 1));

                int owedEffect = (int)(equippedEffect * 0.5f);

                Sprite icon = ResourceManager.Instance.LoadSprite($"Weapon/spear_{weaponCount}");
                EquipmentData data = new EquipmentData(name, icon, level, EquipmentType.Weapon, rarity, owedEffect, equippedEffect, colors[rarityIntValue]);
                weaponList.Add(data);
                weaponCount++;
            }
        }
        equipmentDatas.Add(EquipmentType.Weapon, weaponList);
    }

    private void CreateAllArmor()
    {
        int rarityIntValue = 0;
        int armorCount = 1; 

        foreach (Rarity rarity in rarities)
        {
            if (rarity == Rarity.None) continue;
            rarityIntValue = Convert.ToInt32(rarity);
            for (int level = 1; level <= maxLevel; level++)
            {
                // 11_Armor_Common
                string name = $"{rarityIntValue}{level}_{EquipmentType.Armor}_{rarity}";
                int equippedEffect = level * ((int)Mathf.Pow(10, rarityIntValue + 1));
                int owedEffect = (int)(equippedEffect * 0.5f);
                Sprite icon = ResourceManager.Instance.LoadSprite($"Armor/armor_{armorCount}");
                EquipmentData data = new EquipmentData(name, icon, level, EquipmentType.Armor, rarity, owedEffect, equippedEffect, colors[rarityIntValue]);
                armorList.Add(data);
                armorCount++;
            }
        }
        equipmentDatas.Add(EquipmentType.Armor, armorList);
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
        if ((int)equipment.rarity == rarities.Length - 1 && equipment.level == maxLevel) return -1;

        int compositeCount = equipment.quantity / 4;
        equipment.quantity %= 4;

        EquipmentData nextEquipment = GetNextEquipment(equipment);
        nextEquipment.quantity += compositeCount;
        return compositeCount;
    }

    private EquipmentData GetNextEquipment(EquipmentData equipment)
    {
        List<EquipmentData> datas = GetEquipmentDatas(equipment.type);

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i] == equipment)
                return datas[i + 1];
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

    public string ChangeClassName(Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.Common:
                return "일반";
            case Rarity.Rare:
                return "레어";
            case Rarity.Epic:
                return "에픽";
            case Rarity.Ancient: 
                return "영웅";
            case Rarity.Legendary:
                return "전설";
            case Rarity.Mythology:
                return "신화"; 
        }
        return null; 
    }
}
