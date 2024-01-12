using UnityEngine;

[CreateAssetMenu()]
public class BaseEquipmentSO :ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private Color color; 
    [SerializeField] private int quantity;
    [SerializeField] private int level;
    [SerializeField] private bool onEquipped;
    [SerializeField] private EquipmentType type;
    [SerializeField] private Rarity rarity;
    [SerializeField] private int enhancementLevel;
    [SerializeField] private int baseOwnedEffect;
    [SerializeField] private int baseEquippedEffect;
    [SerializeField] private float increasePercentEffect;
    [SerializeField] private int enhancementMaxLevel;
    [SerializeField] private bool onAwaken;

    public string Name => name;
    public Sprite IconSprite => iconSprite;
    public Color Color => color;    
    public int Quantity => quantity;
    public int Level => level;
    public bool OnEquipped => onEquipped;
    public EquipmentType Type => type;
    public Rarity Rarity => rarity;
    public int EnhancementLevel => enhancementLevel;    

    public int BaseOwnedEffect => baseOwnedEffect;
    public int BaseEquippedEffect => baseEquippedEffect;
    public float IncreasePercentEffect => increasePercentEffect;

    public int EnhancementMaxLevel => enhancementMaxLevel;  
    public  bool OnAwaken => onAwaken;
}
