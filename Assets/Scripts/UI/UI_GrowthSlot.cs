using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthSlot : MonoBehaviour
{
    [SerializeField] Text slotNameText;
    [SerializeField] Text currentLevelText;
    [SerializeField] Text maxLevelText;
    [SerializeField] Text increaseText;

    [SerializeField] Button upgradeButton;
    [SerializeField] Text upgradePriceText;

    private StatusUpgradeData data;


    public void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButton);   
        if(data.baseSo.dataType== DataType.Int)
        {
           // data.OnStatusUpgrade += UpdateSlotUI; 
        }
    }

    public void UpdateSlotUI(StatusUpgradeData data)
    {
        this.data = data;
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
    }

    public void OnUpgradeButton()
    {
        data.UpgradeUpdate();


    }
}
