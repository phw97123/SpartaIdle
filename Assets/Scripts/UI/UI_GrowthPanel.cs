using System.Collections.Generic;
using UnityEngine;

public class UI_GrowthPanel : MonoBehaviour
{
    // tap 

    [SerializeField] private Transform slotContent;
    public readonly string GROWTHSLOT_PATH = "UI/UI_GrowthSlot"; 

    private void Start()
    {
        CreateSlot(StatusUpgradeManager.Instance.GetUpgradeDatas()); 
    }

    private void CreateSlot(List<StatusUpgradeData> datas)
    {
        foreach(StatusUpgradeData data in datas)
        {
            GameObject slotPrefab = ResourceManager.Instance.Instantiate(GROWTHSLOT_PATH, slotContent);
            UI_GrowthSlot slot = slotPrefab.GetComponent<UI_GrowthSlot>();
            slot.transform.SetParent(slotContent);
            slot.UpdateSlotUI(data);
        }
    }    
}
