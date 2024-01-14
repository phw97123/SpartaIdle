using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnhancePopup : UI_Base
{
    [SerializeField] private UI_EquipIconSlot slot;
    [SerializeField] private Text enhanceLevelText;
    [SerializeField] private Text equippedEffectText;
    [SerializeField] private Text ownedEffectText;
    [SerializeField] private Text enhanceStoneCostText;
    [SerializeField] private Button enhanceButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Text currentEnhanceStoneText;

    EquipmentData currentData;

    public event Action OnClosed; 

    private void Awake()
    {
        enhanceButton.onClick.AddListener(OnEnhanceBtn);
        closeButton.onClick.AddListener(CloseUI); 
    }
    public override void OpenUI()
    {
        base.OpenUI();
    }
    public void UpdateUI(EquipmentData data)
    {
        currentData = data; 
        slot.UpdateSlotUI(data);
        enhanceLevelText.text = $"장비 강화 ({data.enhancementLevel} / 1000)";
        switch (data.baseSO.EquipmentType)
        {
            case EquipmentType.Weapon:
                equippedEffectText.text = $"장착 효과 : 피해량 {data.equippedEffect}% ({data.nextEquippedEffect}%) 증가";
                ownedEffectText.text = $"보유 효과 : 피해량 {data.ownedEffect}% ({data.nextOwnedEffect}%) 증가";

                break;

            case EquipmentType.Armor:
                equippedEffectText.text = $"장착 효과 : 체력 {data.equippedEffect}% ({data.nextEquippedEffect}%) 증가";
                equippedEffectText.text = $"보유 효과 : 체력 {data.ownedEffect}% ({data.nextOwnedEffect}%) 증가";
                break;
        }
        enhanceStoneCostText.text = data.enhanceStoneCost.ToString();
    }

    public void OnEnhanceBtn()
    {
       // int currentEnhanceStone =
           int.Parse(CurrencyManager.Instance.GetCurrencyAmount(CurrencyType.EnhanceStone)); 
        //if (currentData.enhancementLevel >= 1000) return;
        //if (currentData.GetEnhanceStone() > currentEnhanceStone) return;
        //CurrencyManager.Instance.SubtractCurrency(CurrencyType.EnhanceStone, currentData.GetEnhanceStone());
        currentData.Enhance();
        UpdateUI(currentData); 
    }

    public override void CloseUI()
    {
        base.CloseUI();
        OnClosed?.Invoke(); 
    }
}
