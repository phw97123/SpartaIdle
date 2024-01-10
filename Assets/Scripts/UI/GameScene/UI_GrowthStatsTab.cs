using System.Collections.Generic;
using UnityEngine;

public class UI_GrowthStatsTab : UI_Base
{
    public readonly string GROWTHSLOT_PATH = "UI/UI_GrowthStatsSlot";

    [SerializeField] private Transform slotContent;

    private List<UI_GrowthStatsSlot> slots; 
    private bool isInit = false; 

    public override void OpenUI()
    {
        base.OpenUI();
        if (!isInit) Init();
        else SetSlots(); 
    }

    private void Init()
    {
        CreateSlot(StatusUpgradeManager.Instance.GetUpgradeDatas());
    }

    private void CreateSlot(List<StatusUpgradeData> datas)
    {
        foreach (StatusUpgradeData data in datas)
        {
            GameObject slotPrefab = ResourceManager.Instance.Instantiate(GROWTHSLOT_PATH, slotContent);
            UI_GrowthStatsSlot slot = slotPrefab.GetComponent<UI_GrowthStatsSlot>();
            slot.transform.SetParent(slotContent);
            slot.Init(data); 
        }
    }

    private void SetSlots()
    {
        foreach (UI_GrowthStatsSlot slot in slots)
            slot.UpdateSlotUI(); 
    }
}
