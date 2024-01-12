
public enum EquipmentType
{
    Weapon,
    Armor,
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Ancient,
    Legendary,
    Mythology,
    None
}

public class EquipmentData 
{
    // TODO : 데이터 정리 중복, 필요없는 데이터 지우기 
    public BaseEquipmentSO baseSO;
    public int quantity;
    public int level;
    public bool OnEquipped;
    public int enhancementLevel;
    public int ownedEffect;
    public int equippedEffect;
    public bool OnAwaken;

    public EquipmentData (BaseEquipmentSO baseSo)
    {
        this.baseSO = baseSo;
        quantity = baseSO.Quantity;
        level = baseSO.Level;
        OnEquipped = baseSO.OnEquipped;
        enhancementLevel = baseSO.EnhancementLevel;
        ownedEffect = baseSo.BaseOwnedEffect; 
        equippedEffect = baseSO.BaseEquippedEffect;
        OnAwaken = baseSO.OnAwaken;
    }
}
