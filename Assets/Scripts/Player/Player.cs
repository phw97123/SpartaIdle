using System;

public class Player : Character
{
    private EquipmentData equiped_Weapon = null;
    private EquipmentData equiped_Armor = null;

    public Action<EquipmentData> OnEquip;
    public Action<EquipmentType> OnUnEquip;

    private void Start()
    {
        OnEquip += Equip; 
        OnUnEquip += UnEquip;
    }

    public void Equip(EquipmentData equipData)
    {
        switch (equipData.type)
        {
            case EquipmentType.Weapon:
                UnEquip(equipData.type);
                equiped_Weapon = equipData;
                equiped_Weapon.isEquipped = true;
                break;
            case EquipmentType.Armor:
                UnEquip(equipData.type);
                equiped_Armor = equipData;
                equiped_Armor.isEquipped = true;
                break;
        }
    }

    public void UnEquip(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                if (equiped_Weapon == null) return;
                equiped_Weapon.isEquipped = false;
                equiped_Weapon = null;
                break;
            case EquipmentType.Armor:
                if (equiped_Armor == null) return;
                equiped_Armor.isEquipped = false;
                equiped_Armor = null;
                break;
        }
    }
}
