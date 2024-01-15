using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_SummonSlot : UI_Base
{
    [SerializeField] private Text slotType;
    [SerializeField] private Text slotLevel;
    [SerializeField] private Slider summonExpBar;
    [SerializeField] private Text summonExpText;
    [SerializeField] private Text summonPriceText;
    [SerializeField] private Button percentageInfoButton;
    [SerializeField] private Button summonButton;
    [SerializeField] private Button adSummonButton;

    public SummonData summonData;
    private int number = 1;

    private CurrencyManager currencyManager;

    public void Init(SummonData data)
    {
        currencyManager = CurrencyManager.Instance;
        summonData = data;
        InitBtnEvent(); 
    }

    private void InitBtnEvent()
    {
        summonButton.onClick.AddListener(OnSummonButton); 
        percentageInfoButton.onClick.AddListener(OnPercentageInfoButton);
    }

    public void UpdateSlotUI()
    {
        slotType.text = $"{summonData.GetTypeName(summonData.Type)} º“»Ø";
        slotLevel.text = $"Lv.{summonData.summonLevel}";
        summonExpBar.value = (float)summonData.summonCurrentExp / summonData.summonMaxExp;
        summonExpText.text = $"{summonData.summonCurrentExp}/{summonData.summonMaxExp}";

        if (int.Parse(currencyManager.GetCurrencyAmount(CurrencyType.Dia)) < summonData.baseSummonPrice * number)
        {
            summonPriceText.color = Color.red;
            summonButton.interactable = false;
        }
        else
        {
            summonPriceText.color = Color.white;
            summonButton.interactable = true;
        }
        summonPriceText.text = $"{summonData.baseSummonPrice * number}";
    }

    public void ChangeSummonPrice(int newNumber)
    {
        number = newNumber;
        summonPriceText.text = $"{500 * number}";
    }

    private void OnPercentageInfoButton()
    {

    }

    private void OnSummonButton()
    {

    }
}
