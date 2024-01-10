using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthStatsSlot : UI_Base
{
    [SerializeField] Text slotNameText;
    [SerializeField] Text currentLevelText;
    [SerializeField] Text maxLevelText;
    [SerializeField] Text increaseText;

    [SerializeField] Button upgradeButton;
    [SerializeField] Text upgradePriceText;

    private StatusUpgradeData data;

    private CurrencyManager currencyManager;

    public void Init(StatusUpgradeData data)
    {
        currencyManager = CurrencyManager.Instance;

        this.data = data;
        upgradeButton.onClick.AddListener(OnUpgradeButton);
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        slotNameText.text = $"{data.baseSo.upgradeName}";
        currentLevelText.text = $"Lv.{data.currentUpgradeLevel}";
        maxLevelText.text = $"Max Lv.{data.maxUpgradeLevel}";
        // TODO : 한국 단위로 변경
        upgradePriceText.text = $"{data.upgradePrice}";
        if (data.upgradeValue == null)
        {
            increaseText.text = $"{data.percentUpgradeValue:0.00}";
            return;
        }
        increaseText.text = $"{data.upgradeValue}";

        SetButtonInteractable();
    }

    public void OnUpgradeButton()
    {
        if (!CurrencyManager.Instance.SubtractCurrency(data.baseSo.currencyType, data.upgradePrice)) return;

        data.UpgradeUpdate();
        UpdateSlotUI();
    }

    private void SetButtonInteractable()
    {
        if (data.upgradePrice <= int.Parse(currencyManager.GetCurrencyAmount(data.baseSo.currencyType)) && data.currentUpgradeLevel < data.maxUpgradeLevel)
        {
            upgradePriceText.color = Color.white;
            upgradeButton.interactable = true;
        }
        else
        {
            if (data.currentUpgradeLevel < data.maxUpgradeLevel)
                upgradePriceText.color = Color.red;

            upgradeButton.interactable = false;
        }
    }
}
