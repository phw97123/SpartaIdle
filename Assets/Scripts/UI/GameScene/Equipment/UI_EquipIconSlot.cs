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

    private EquipmentData equipData;

    public void Init(EquipmentData data)
    {
        this.equipData = data;
        selectSlot.onValueChanged.AddListener(isOn =>
        {
            // 선택 이미지
            if (isOn) checkIcon.gameObject.SetActive(true);
            else checkIcon.gameObject.SetActive(false);
            // 선택 정보 띄우기 
            //SelectedSlotInfo(); 
        });

        UpdateSlotUI(equipData); 
    }

    public void UpdateSlotUI(EquipmentData data)
    {
        rarityText.text = $"{data.baseSO.Name} {data.baseSO.Level}";
        icon.sprite = data.baseSO.IconSprite;
        background.color = data.baseSO.Color;
        valueText.text = $"{data.quantity}/4";
        sliderbar.value = data.quantity / 4;
        enhancementLevelText.text = $"Lv.{data.enhancementLevel}"; 
    }

    public void SelectedSlotInfo()
    {

    }
}
