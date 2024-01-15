using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Ancient,
    Legendary,
    Mythology,
    None
}

public class EquipmentData 
{
    // TODO : 데이터 정리 -> 중복, 필요없는 데이터 지우기 
    // public BaseEquipmentSO baseSO;

    public string name;
    public Sprite icon; 
    public int quantity;
    public int level;
    public bool isEquipped;
    public EquipmentType type;
    public Rarity rarity; 
    public int enhancementLevel;
    public int baseOwnedEffect;
    public int baseEquippedEffect;
    public int ownedEffect;
    public int equippedEffect;
    public int enhancementMaxLevel = 1000;
    public Color myColor; 

    public EquipmentData (string name, Sprite icon,int level, EquipmentType type, Rarity rarity, int baseOwnedEffect, int baseEquippedEffect,Color myColor)
    {
        quantity = 0;
        isEquipped = false;
        enhancementLevel = 1;   

        this.name = name; 
        this.icon = icon;
        this.level = level;
        this.type = type;
        this.rarity = rarity;
        this.baseOwnedEffect = baseOwnedEffect;
        this.baseEquippedEffect = baseEquippedEffect; 
        this.myColor = myColor;

        ownedEffect = baseOwnedEffect;
        equippedEffect = baseEquippedEffect;
    }

    public virtual void Enhance()
    {
        equippedEffect += baseEquippedEffect;
        ownedEffect += baseOwnedEffect;
        enhancementLevel++;
    }

    public int GetEnhanceStone()
    {
        var requipredEnhanceStone = equippedEffect - baseOwnedEffect;
        return requipredEnhanceStone;
    }
}
