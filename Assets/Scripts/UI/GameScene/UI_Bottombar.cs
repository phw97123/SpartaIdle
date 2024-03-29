using UnityEngine;
using UnityEngine.UI;

public class UI_Bottombar : UI_Base
{
    [SerializeField] private Button storeButton;
    [SerializeField] private Button growthButton;
    [SerializeField] private Button summonButton;
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button skillButton;
    [SerializeField] private Button dungeonButton;

    private UIManager uiManager;

    private UI_GrowthPanel uiGrowthPanel; 
    private UI_EquipmentPanel uiEquipmentPanel;
    private UI_SummonPanel uiSummonPanel;

    private void Awake()
    {
        uiManager = UIManager.Instance;

        growthButton.onClick.AddListener(OnGrowthPanel);
        equipmentButton.onClick.AddListener(OnEquipmentPanel); 
        summonButton.onClick.AddListener(OnSummonPanel);
    }

    private void OnGrowthPanel()
    {
        if (uiManager.TryGetUIComponent<UI_GrowthPanel>(out uiGrowthPanel))
            uiGrowthPanel.OpenUI();
    }

    private void OnEquipmentPanel()
    {
        if(uiManager.TryGetUIComponent<UI_EquipmentPanel>(out uiEquipmentPanel))
            uiEquipmentPanel.OpenUI();
    }

    private void OnSummonPanel()
    {
        if(uiManager.TryGetUIComponent<UI_SummonPanel>(out uiSummonPanel))
            uiSummonPanel.OpenUI();
    }
}
