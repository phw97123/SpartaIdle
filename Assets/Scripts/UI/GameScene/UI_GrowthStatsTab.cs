using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthStatsTab : UI_Base
{
    public readonly string GROWTHSLOT_PATH = "UI/UI_GrowthStatsSlot";

    [SerializeField] private Transform slotContent;

    [SerializeField] private Toggle minToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle maxToggle;

    [SerializeField] private Text minNumText;
    [SerializeField] private Text mediumNumText;
    [SerializeField] private Text maxNumText;

    const int min = 1, medium = 5, max = 10;

    private List<UI_GrowthStatsSlot> slots = new List<UI_GrowthStatsSlot>();
    private bool isInit = false;

    private int currentToggle = min; 

    public override void OpenUI()
    {
        base.OpenUI();

        if (!isInit) Init();
        else SetSlots();
    }

    private void Init()
    {
        CreateSlot(StatusUpgradeManager.Instance.GetUpgradeDatas());

        minToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(min);
        });
        mediumToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(medium);
        });
        maxToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(max);
        });

        minNumText.text = $"X{min}";
        mediumNumText.text = $"X{medium}";
        maxNumText.text = $"X{max}";
    }

    private void CreateSlot(List<StatusUpgradeData> datas)
    {
        foreach (StatusUpgradeData data in datas)
        {
            GameObject slotPrefab = ResourceManager.Instance.Instantiate(GROWTHSLOT_PATH, slotContent);
            UI_GrowthStatsSlot slot = slotPrefab.GetComponent<UI_GrowthStatsSlot>();
            slot.transform.SetParent(slotContent);
            slot.Init(data);
            slots.Add(slot);
        }

        foreach (UI_GrowthStatsSlot slot in slots)
        {
            slot.OnButton += SetAllButton;
        }
    }

    private void SetSlots()
    {
        foreach (UI_GrowthStatsSlot slot in slots)
        {
            slot.UpdateSlotUI();
        }
    }

    private void SetAllButton()
    {
        foreach (UI_GrowthStatsSlot slot in slots)
        {
            slot.SetButtonInteractable();
        }
    }

    private void NumberToggle(int num)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ChangeUpgradePrice(num);
        }
        SetAllButton(); 
    }
}
