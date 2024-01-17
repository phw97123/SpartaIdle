using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UI_SummonPanel : UI_Base
{
    [SerializeField] private Toggle minToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle maxToggle;

    [SerializeField] private Text minNumText;
    [SerializeField] private Text mediumNumText;
    [SerializeField] private Text maxNumText;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button backgroundPanel;
    [SerializeField] private UI_SummonSlot[] slots;
    [SerializeField] private SummonData[] datas; 

    private readonly int min = 1, med = 10, max = 30;

    private bool isInit = false;

    public override void OpenUI()
    {
        base.OpenUI();
        if (!isInit) Init();
    }

    private void Init()
    {
        isInit = true;

        minToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(min);
        });
        mediumToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(med);
        });
        maxToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn) NumberToggle(max);
        });

        InitBtnEvent();

        datas = SummonManager.Instance.GetSummonDatats();
        CreateSlots(datas); 
    }

    private void CreateSlots(SummonData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            slots[i].Init(datas[i]);
        }
    }

    private void InitBtnEvent()
    {
        closeButton.onClick.AddListener(CloseUI);
        backgroundPanel.onClick.AddListener(CloseUI);
    }

    private void NumberToggle(int num)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ChangeSummonPrice(num);
        }
    }
}
