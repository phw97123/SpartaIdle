using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [SerializeField] private ToggleGroup slotToggleGroup;
    [SerializeField] private ToggleGroup tabToggleGroup;

    [SerializeField] private Text equipmentName;
    [SerializeField] private UI_EquipIconSlot currentSlot;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Text equipmentEffect;
    [SerializeField] private Text nextEquipmentEffect;
    [SerializeField] private Text ownedEffect;
    [SerializeField] private Text nextOwnedEffect;

    [SerializeField] private Button equipBtn;
    [SerializeField] private Button unEquipBtn;
    [SerializeField] private Button autoEquipBtn;
    [SerializeField] private Button compositeBtn;
    [SerializeField] private Button allCompositeBtn;
    [SerializeField] private Button enhancePanelBtn;

    private List<UI_EquipIconSlot> slots = new List<UI_EquipIconSlot>();

    private bool isInit = false;
    private EquipmentType currentTabType = EquipmentType.Weapon;

    private EquipmentData selectedData;

    private EquipmentManager equipmentManager;

    private UI_EnhancePopup uiEnhancePopup;

    public override void OpenUI()
    {
        base.OpenUI();
        if (!isInit) Init();
        else
            AllSlotUpdate(currentTabType);

        slotToggleGroup.GetFirstActiveToggle();
    }

    public void Init()
    {
        isInit = true;
        equipmentManager = EquipmentManager.Instance;

        CreateSlot(EquipmentManager.Instance.GetEquipmentDatas(EquipmentType.Weapon));
        weaponTab.group = tabToggleGroup;
        armorTab.group = tabToggleGroup;

        // 맨 처음 선택되는 슬롯 설정 
        weaponTab.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                AllSlotUpdate(EquipmentType.Weapon);
                SelectSlotInfo(selectedData);
            }
        });

        armorTab.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                AllSlotUpdate(EquipmentType.Armor);
                SelectSlotInfo(selectedData);
            }
        });

        selectedData = slots[0].equipData;
        slots[0].selectSlot.isOn = true;

        uiEnhancePopup = UIManager.Instance.GetUIComponent<UI_EnhancePopup>();
        uiEnhancePopup.OnClosed += OnEnhancePanelClose;
        InitBtnEvent();
    }

    private void InitBtnEvent()
    {
        closeButton.onClick.AddListener(CloseUI);
        backgroundPanel.onClick.AddListener(CloseUI);
        equipBtn.onClick.AddListener(OnEquipBtn);
        unEquipBtn.onClick.AddListener(OnUnEquipBtn);
        autoEquipBtn.onClick.AddListener(OnAutoEquipBtn);
        compositeBtn.onClick.AddListener(OnCompositeBtn);
        allCompositeBtn.onClick.AddListener(OnAllCompositeBtn);
        enhancePanelBtn.onClick.AddListener(OnEnhancePanelBtn);
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
            slot.selectSlot.group = slotToggleGroup;
            slot.onSelectedSlot += SelectSlotInfo;
        }
    }

    private void AllSlotUpdate(EquipmentType type)
    {
        List<EquipmentData> dataList = EquipmentManager.Instance.GetEquipmentDatas(type);
        for (int i = 0; i < dataList.Count; i++)
        {
            slots[i].UpdateSlotUI(dataList[i]);
        }

        foreach (var slot in slots)
        {
            if (slot.equipData.isEquipped)
            {
                selectedData = slot.equipData;
                slot.selectSlot.isOn = true;
                return;
            }
        }

        //selectedData = slots[0].equipData;
        //slots[0].selectSlot.isOn = true;
    }

    private void SelectSlotInfo(EquipmentData data)
    {
        selectedData = data;
        equipmentName.text = data.name;
        currentSlot.UpdateSlotUI(data);
        currentLevel.text = data.enhancementLevel.ToString();

        switch (data.type)
        {
            case EquipmentType.Weapon:
                equipmentEffect.text = $"피해량+{data.equippedEffect}%";
                nextEquipmentEffect.text = $"피해량+{data.equippedEffect + data.baseEquippedEffect}%";

                ownedEffect.text = $"피해량+{data.ownedEffect}%";
                nextOwnedEffect.text = $"피해량+{data.ownedEffect + data.baseOwnedEffect}%";
                break;

            case EquipmentType.Armor:
                equipmentEffect.text = $"체력+{data.equippedEffect}%";
                nextEquipmentEffect.text = $"체력+{data.equippedEffect + data.baseEquippedEffect}%";

                ownedEffect.text = $"체력+{data.ownedEffect}%";
                nextOwnedEffect.text = $"체력+{data.ownedEffect + data.baseOwnedEffect}%";
                break;
        }

        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    private void OnEquipBtn()
    {
        prevPlayer.Instance.OnEquip?.Invoke(selectedData);
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    void SetOnEquippedBtnUI(bool IsEquipped)
    {
        equipBtn.gameObject.SetActive(!IsEquipped);
        unEquipBtn.gameObject.SetActive(IsEquipped);
    }

    private void OnUnEquipBtn()
    {
        prevPlayer.Instance.OnUnEquip?.Invoke(selectedData.type);
        selectedData.isEquipped = false;
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    private void OnAutoEquipBtn()
    {
        EquipmentData data = equipmentManager.AutoEquip(selectedData.type);
        prevPlayer.Instance.OnEquip?.Invoke(data);
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    private void OnEnhancePanelBtn()
    {
        uiEnhancePopup.OpenUI();
        uiEnhancePopup.UpdateUI(selectedData);
    }

    private void OnEnhancePanelClose()
    {
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    private void OnCompositeBtn()
    {
        equipmentManager.Composite(selectedData);
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }

    private void OnAllCompositeBtn()
    {
        equipmentManager.AllComposite(selectedData.type);
        AllSlotUpdate(selectedData.type);
        SetOnEquippedBtnUI(selectedData.isEquipped);
    }
}
