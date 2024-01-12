using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentPanel : UI_Base
{
    public readonly string EQUIPMENT_PATH = "UI/UI_EquipIconSlot";

    [SerializeField] private Toggle weaponTab;
    [SerializeField] private Toggle armorTab;
    [SerializeField] private Transform slotContent;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button backgroundPanel;
    [SerializeField] private ToggleGroup toggleGroup; 
    
    private List<UI_EquipIconSlot> slots = new List<UI_EquipIconSlot>();

    private bool isInit = false;
    private EquipmentType currentTabType = EquipmentType.Weapon;

    public override void OpenUI()
    {
        base.OpenUI();
        if (!isInit) Init();
        else
            AllSlotUpdate(currentTabType);

        toggleGroup.GetFirstActiveToggle(); 
    }

    public void Init()
    {
        isInit = true;
        CreateSlot(EquipmentManager.Instance.GetEquipmentDatas(EquipmentType.Weapon));

        // 맨 처음 선택되는 슬롯 설정 
        weaponTab.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                // 모든 정보들 데이터 변경
                AllSlotUpdate(EquipmentType.Weapon);
            }
        });

        armorTab.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                // 모든 정보들 데이터 변경
                AllSlotUpdate(EquipmentType.Armor);
            }
        });

        closeButton.onClick.AddListener(CloseUI);
        backgroundPanel.onClick.AddListener(CloseUI);

        slots[0].selectSlot.isOn = true;
    }

    private void CreateSlot(List<EquipmentData> datas)
    {
        foreach (EquipmentData data in datas)
        {
            GameObject slotPrefab = ResourceManager.Instance.Instantiate(EQUIPMENT_PATH, slotContent);
            UI_EquipIconSlot slot = slotPrefab.GetComponent<UI_EquipIconSlot>();
            slot.transform.SetParent(slotContent);
            slot.Init(data); 
            slots.Add(slot);
            slot.selectSlot.group = toggleGroup;
        }
    }

    private void AllSlotUpdate(EquipmentType type)
    {
        List<EquipmentData> dataList = EquipmentManager.Instance.GetEquipmentDatas(type);
        for (int i = 0; i < dataList.Count; i++)
        {
            slots[i].UpdateSlotUI(dataList[i]);
        }
        slots[0].selectSlot.isOn = true;
    }
}
