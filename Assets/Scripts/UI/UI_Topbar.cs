using UnityEngine;
using UnityEngine.UI;

public class UI_Topbar : MonoBehaviour
{
    [Header("PlayerData")]
    [SerializeField] private Text nameText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Text levelText;
    [SerializeField] private Text expPercentageText;
    [SerializeField] private Slider expSlider;

    [Header("CurrencyData")]
    [SerializeField] private Text goldText;
    [SerializeField] private Text diaText; 

    private PlayerData playerData;

    private void Start()
    {
        playerData = Player.instance.playerData;
        SetupEventListeners();

        UpdateUI(); 
    }

    private void SetupEventListeners()
    {
        playerData.OnExpChanged += UpdateUI;
        playerData.OnLevelChanged += UpdateUI;
    }

    public void UpdateUI()
    {
        nameText.text = $"{playerData.name}";
        iconImage = playerData.iconImage; 
        levelText.text = $"Lv.{playerData.level}";

        float percentage = (float)playerData.currentExp / playerData.maxExp *100; 
        expPercentageText.text = $"EXP {percentage:F2}%";
        expSlider.value = percentage /100;

        // goldText.text = $"";
        // diaText.text = $""; 
    }
}
