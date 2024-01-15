using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthPanel : UI_Base
{
    [SerializeField] private Toggle statsTab;
    [SerializeField] private Toggle Tab1;
    [SerializeField] private Toggle Tab2;
    [SerializeField] private Toggle Tab3;
    [SerializeField] private Toggle Tab4;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button backgroundPanel;

    [SerializeField] private UI_GrowthStatsTab uiGrowthStatsTab; 

    private void Awake()
    {
        uiGrowthStatsTab.OpenUI(); 
        statsTab.onValueChanged.AddListener(isOn=>
        {
            if (isOn) uiGrowthStatsTab.OpenUI(); 
            else uiGrowthStatsTab.CloseUI();
        });

        closeButton.onClick.AddListener(CloseUI); 
        backgroundPanel.onClick.AddListener(CloseUI);
    }
}
