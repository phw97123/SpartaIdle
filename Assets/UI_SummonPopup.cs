using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_SummonPopup : UI_Base
{
    private readonly int SUMMON_EXP = 1;
    private readonly int SUMMON_PRICE = 50;

    [SerializeField] private Text levelText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text currentDiaText;
    [SerializeField] private Text currentExpText;
    [SerializeField] private Slider currentExpbar;

    [SerializeField] private Transform slotContent;

    [SerializeField] private Button summonButton;
    [SerializeField] private Button closeButton;

    private List<UI_EquipmentIconSlot> slotPools = new List<UI_EquipmentIconSlot>();

    private SummonData summonData;
    private int count;

    private SummonManager summonManager;
    private CurrencyManager currencyManager;

    private bool isInit = false;

    public override void OpenUI()
    {
        base.OpenUI();
        if (!isInit) Init();
    }

    private void Init()
    {
        isInit = true;
        summonManager = SummonManager.Instance;
        currencyManager = CurrencyManager.Instance;

        summonButton.onClick.AddListener(ReSummon);
        closeButton.onClick.AddListener(CloseUI);
    }

    public void UpdateUI(SummonData data)
    {
        summonData = data;
        levelText.text = $"Lv.{data.summonLevel}";
        typeText.text = $"{data.GetTypeName()} º“»Ø";
        currentDiaText.text = currencyManager.GetCurrencyAmount(CurrencyType.Dia);
        currentExpbar.value = (float)data.summonCurrentExp / data.summonMaxExp;
        currentExpText.text = $"{data.summonCurrentExp}/{data.summonMaxExp}";
    }

    private UI_EquipmentIconSlot Get()
    {
        UI_EquipmentIconSlot selectSlot = null;
        foreach (var slot in slotPools)
        {
            if (!slot.gameObject.activeSelf)
            {
                selectSlot = slot;
                slot.gameObject.SetActive(true);
                break;
            }
        }

        if (!selectSlot)
        {
            selectSlot = CreateSlot();
        }
        return selectSlot;
    }

    private UI_EquipmentIconSlot CreateSlot()
    {
        GameObject slotPrefab = ResourceManager.Instance.Instantiate("UI/UI_EquipmentIconSlot", slotContent);
        UI_EquipmentIconSlot slot = slotPrefab.GetComponent<UI_EquipmentIconSlot>();
        slotPools.Add(slot);
        return slot;
    }

    public void OnSummon(SummonData data, int count)
    {
        summonData = data;
        this.count = count;

        ReturnToPools();

        List<EquipmentData> summondataList = summonManager.SummonEquipment(data, count);

        for (int i = 0; i < summondataList.Count; i++)
        {
            UI_EquipmentIconSlot slot = Get();
            slot.UpdateSlotUI(summondataList[i]);
        }

        currencyManager.SubtractCurrency(CurrencyType.Dia, SUMMON_PRICE * count);
        data.UpdateExp(SUMMON_EXP * count);
    }

    public void ReSummon()
    {
        ReturnToPools();
        OnSummon(summonData, count);
        UpdateUI(summonData);
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    private void ReturnToPools()
    {
        foreach (var slot in slotPools)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
