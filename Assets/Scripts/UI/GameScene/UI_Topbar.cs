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
    [SerializeField] private Text[] currencyText;

    private PlayerData playerData;
    private CurrencyManager currencyManager;

    private void Awake()
    {
        currencyManager = CurrencyManager.Instance;
    }
    private void Start()
    {
        playerData = Player.Instance.playerData;
        SetupEventListeners();
        UpdatePlayerInfoUI();

        foreach (CurrencyData currencyData in currencyManager.currencyDatas)
        {
            if (currencyData.currencyType == CurrencyType.EnhanceStone) continue; 
            UpdatecurrencyUI(currencyData.currencyType, currencyData.amount);
        }
    }

    private void SetupEventListeners()
    {
        playerData.OnExpChanged += UpdatePlayerInfoUI;
        playerData.OnLevelChanged += UpdatePlayerInfoUI;
        currencyManager.OnCurrencyChanged += UpdatecurrencyUI;
    }

    public void UpdatePlayerInfoUI()
    {
        nameText.text = $"{playerData.name}";
        iconImage = playerData.iconImage;
        levelText.text = $"Lv.{playerData.level}";

        float percentage = (float)playerData.currentExp / playerData.maxExp * 100;
        expPercentageText.text = $"EXP {percentage:F2}%";
        expSlider.value = percentage / 100;
    }

    public void UpdatecurrencyUI(CurrencyType type, string amount)
    {
        CurrencyData currency = currencyManager.currencyDatas.Find(c => c.currencyType == type);
        currencyText[(int)type].text = amount;
    }
}
