using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentIconSlot : UI_Base
{
    [SerializeField] private Text rarityText;
    [SerializeField] private Image icon;
    [SerializeField] private Image background;

    public void UpdateSlotUI(EquipmentData data)
    {
        rarityText.text = $"{EquipmentManager.Instance.ChangeClassName(data.rarity)} {data.level}";
        icon.sprite = data.icon;
        background.color = data.myColor;
    }

    public void ResetSlot()
    {
        gameObject.SetActive(false);     
    }
}
