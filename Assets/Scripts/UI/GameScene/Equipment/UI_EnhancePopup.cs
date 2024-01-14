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
        enhanceLevelText.text = $"��� ��ȭ ({data.enhancementLevel} / 1000)";
        switch (data.baseSO.EquipmentType)
        {
            case EquipmentType.Weapon:
                equippedEffectText.text = $"���� ȿ�� : ���ط� {data.equippedEffect}% ({data.nextEquippedEffect}%) ����";
                ownedEffectText.text = $"���� ȿ�� : ���ط� {data.ownedEffect}% ({data.nextOwnedEffect}%) ����";

                break;

            case EquipmentType.Armor:
                equippedEffectText.text = $"���� ȿ�� : ü�� {data.equippedEffect}% ({data.nextEquippedEffect}%) ����";
                equippedEffectText.text = $"���� ȿ�� : ü�� {data.ownedEffect}% ({data.nextOwnedEffect}%) ����";
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
