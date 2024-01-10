using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthPanel : UI_Base
{
    [SerializeField] private Toggle statsTab;
    //[SerializeField] private Toggle Tab1;
    //[SerializeField] private Toggle Tab2;
    //[SerializeField] private Toggle Tab3;
    //[SerializeField] private Toggle Tab4;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _backgroundPanel;

    [SerializeField] private UI_GrowthStatsTab uiGrowthStatsTab; 

    private void Awake()
    {
        statsTab.isOn = true;
        uiGrowthStatsTab.OpenUI();
        statsTab.onValueChanged.AddListener(isOn=>
        {
            if (isOn) uiGrowthStatsTab.OpenUI(); 
            else uiGrowthStatsTab.CloseUI();
        });

        _closeButton.onClick.AddListener(CloseUI); 
        _backgroundPanel.onClick.AddListener(CloseUI);
    }
}
