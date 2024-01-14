
using System.Buffers.Text;
using System.Numerics;
using UnityEditor.Recorder.FrameCapturer;

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
    // TODO : 데이터 정리 -> 중복, 필요없는 데이터 지우기 
    public BaseEquipmentSO baseSO;
    public int quantity;
    public int level;
    public bool OnEquipped;
    public int enhancementLevel;
    public int ownedEffect;
    public int equippedEffect;
    public int nextOwnedEffect;
    public int nextEquippedEffect;
    public bool OnAwaken;
    public int enhanceStoneCost;

    public EquipmentData (BaseEquipmentSO baseSO)
    {
        this.baseSO = baseSO;
        quantity = this.baseSO.Quantity;
        level = this.baseSO.Level;
        OnEquipped = this.baseSO.OnEquipped;
        enhancementLevel = this.baseSO.EnhancementLevel;
        ownedEffect = this.baseSO.BaseOwnedEffect;
        equippedEffect = this.baseSO.BaseEquippedEffect;
        nextOwnedEffect = this.baseSO.NextOwnedEffect;
        nextEquippedEffect = this.baseSO.NextEquippedEffect;
        OnAwaken = this.baseSO.OnAwaken;
        enhanceStoneCost = this.baseSO.EnhanceStoneCost;
    }

    public virtual void Enhance()
    {
        equippedEffect += baseSO.BaseOwnedEffect;
        ownedEffect += baseSO.BaseOwnedEffect;

        enhancementLevel++;
    }

    public int GetEnhanceStone()
    {
        var requipredEnhanceStone = equippedEffect - baseSO.BaseOwnedEffect;
        return requipredEnhanceStone;
    }
}
