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

    private void Awake()
    {
        uiManager = UIManager.Instance;

        growthButton.onClick.AddListener(OnGrowthPanel); 
    }

    private void OnGrowthPanel()
    {
        if (uiManager.TryGetUIComponent<UI_GrowthPanel>(out uiGrowthPanel))
            uiGrowthPanel.OpenUI();
    }
}
