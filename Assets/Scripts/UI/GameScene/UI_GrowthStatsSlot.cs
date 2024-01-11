using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthStatsSlot : UI_Base
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text slotNameText;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text maxLevelText;
    [SerializeField] private Text increaseText;

    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradePriceText;

    [SerializeField] private Sprite[] sprites;
    private StatusUpgradeData data;
    private CurrencyManager currencyManager;
    public Action OnButton;

    private int number = 1;

    public void Init(StatusUpgradeData data)
    {
        currencyManager = CurrencyManager.Instance;

        this.data = data;
        upgradeButton.onClick.AddListener(OnUpgradeButton);
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        iconImage.sprite = sprites[(int)data.baseSo.currencyType];
        slotNameText.text = $"{data.baseSo.upgradeName}";
        currentLevelText.text = $"Lv.{data.currentUpgradeLevel}";
        maxLevelText.text = $"Max Lv.{data.maxUpgradeLevel}";
        // TODO : 한국 단위로 변경
        upgradePriceText.text = $"{data.upgradePrice}";
        if (data.upgradeValue == 0)
        {
            increaseText.text = $"{data.percentUpgradeValue:F2}";
        }
        else
            increaseText.text = $"{data.upgradeValue}";

        SetButtonInteractable();
    }

    public void OnUpgradeButton()
    {
        if (!CurrencyManager.Instance.SubtractCurrency(data.baseSo.currencyType, data.upgradePrice)) return;

        for (int i = 0; i < number; i++)
        {
            data.UpgradeUpdate();
            UpdateSlotUI();
            OnButton?.Invoke();
        }
    }

    public void SetButtonInteractable()
    {
        if (data.upgradePrice <= int.Parse(currencyManager.GetCurrencyAmount(data.baseSo.currencyType)))
        {
            upgradePriceText.color = Color.white;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradePriceText.color = Color.red;
            upgradeButton.interactable = false;
        }

        if (data.currentUpgradeLevel >= data.maxUpgradeLevel)
        {
            upgradePriceText.color = Color.white;
            upgradePriceText.text = "0";
            upgradeButton.interactable = false;
        }
    }

    public void ChangeUpgradePrice(int newnNumber)
    {
        number = newnNumber;
        upgradePriceText.text = $"{data.upgradePrice*number+number+1}";
    }
}
