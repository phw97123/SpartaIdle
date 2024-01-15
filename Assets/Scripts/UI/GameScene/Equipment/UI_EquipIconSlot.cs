using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipIconSlot : UI_Base
{
    public Toggle selectSlot;

    [SerializeField] private Text rarityText;
    [SerializeField] private Text valueText;
    [SerializeField] private Image icon;
    [SerializeField] private Image checkIcon;
    [SerializeField] private Image background;
    [SerializeField] private Slider sliderbar;
    [SerializeField] private GameObject equipLabel;
    [SerializeField] private Text enhancementLevelText; 

    public EquipmentData equipData;

    public Action<EquipmentData> onSelectedSlot; 

    public void Init(EquipmentData data)
    {
        equipData = data;
        selectSlot.onValueChanged.AddListener(isOn =>
        {
            if (isOn) checkIcon.gameObject.SetActive(true);
            else checkIcon.gameObject.SetActive(false);

           SelectedSlotInfo(); 
        });

        UpdateSlotUI(equipData); 
    }

    public void UpdateSlotUI(EquipmentData data)
    {
        equipData = data;
        rarityText.text = $"{EquipmentManager.Instance.ChangeClassName(data.rarity)} {data.level}";
        icon.sprite = data.icon;
        background.color = data.myColor; 
        valueText.text = $"{data.quantity}/4";
        sliderbar.value = (float)data.quantity / 4f;
        enhancementLevelText.text = $"Lv.{data.enhancementLevel}";
        equipLabel.SetActive(data.isEquipped);
    }

    public void SelectedSlotInfo()
    {
        onSelectedSlot?.Invoke(equipData); 
    }
}
