using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.XR;
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
    [SerializeField] private Toggle autoSummonToggle;

    private List<UI_EquipmentIconSlot> slotPools = new List<UI_EquipmentIconSlot>();

    private SummonData summonData;
    private int count;

    private WaitForSeconds waitForDrawSlot = new WaitForSeconds(0.05f);
    private WaitForSeconds waitForAutoSummon = new WaitForSeconds(2f);

    private SummonManager summonManager;
    private CurrencyManager currencyManager;

    private bool isAutoSummon = false;
    private bool isSummoning = false; 

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

        summonButton.onClick.AddListener(OnReSummon);
        closeButton.onClick.AddListener(CloseUI);
        autoSummonToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) isAutoSummon = true; 
            else isAutoSummon = false;
        });
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
        StartCoroutine(DrawSummonSlot(data, count));
    }

    private IEnumerator DrawSummonSlot(SummonData data, int count)
    {
        summonData = data;
        this.count = count;

        ReturnToPools();
        List<EquipmentData> summondataList = summonManager.SummonEquipment(data, count);

        for (int i = 0; i < summondataList.Count; i++)
        {
            isSummoning = true; 
            UI_EquipmentIconSlot slot = Get();
            slot.UpdateSlotUI(summondataList[i]);
            slot.gameObject.SetActive(true);
            yield return waitForDrawSlot;
        }

        currencyManager.SubtractCurrency(CurrencyType.Dia, SUMMON_PRICE * count);
        data.UpdateExp(SUMMON_EXP * count);
        UpdateUI(summonData);
    }

    public void OnReSummon()
    {
        if (isAutoSummon)
        {
            StartCoroutine(AutoSummon());
        }
        else
        {
            StartCoroutine(DrawSummonSlot(summonData, count));
        }
    }

    IEnumerator AutoSummon()
    {
        while (isAutoSummon)
        {
            ReturnToPools();
            OnSummon(summonData, count);
            yield return waitForAutoSummon;
        }
    }

    private void ReturnToPools()
    {
        foreach (var slot in slotPools)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public override void CloseUI()
    {
        if (isSummoning) return; 
        base.CloseUI();
    }
}
