using System.Numerics;
using UnityEngine;

[CreateAssetMenu()]
public class BaseStatusUpgradeSO : ScriptableObject
{
    [SerializeField] private StatusType statusType;
    [SerializeField] public DataType dataType;
    [SerializeField] public CurrencyType currencyType;

    [Header("[�ɷ�ġ �̸�]")]
    [SerializeField] public string upgradeName;

    [Header("[����, �ִ� ����]")]
    [SerializeField] private int baseUpgradeLevel;
    [SerializeField] private int maxUpgradeLevel;

    [Header("[�ʱ� ����, ���� ������]")]
    [SerializeField] private int baseUpgradePrice;
    [SerializeField] private int pricePercent;

    [Header("[int �ʱ� ��, ������]")]
    [SerializeField] private int upgradeValue;
    [SerializeField] private int increase;

    [Header("[float �ʱ� ��, ������]")]
    [SerializeField] private float percentUpgradeValue;
    [SerializeField] private float percentIncrease;

    public StatusType StatusType => statusType; 
    public int CurrentUpgradeLevel => baseUpgradeLevel;
    public int MaxUpgradeLevel => maxUpgradeLevel;
    public int UpgradePrice => baseUpgradePrice;
    public int PricePercent => pricePercent;
    public int UpgradeValue => upgradeValue;
    public int Increase => increase;
    public float PercentUpgradeValue => percentUpgradeValue;
    public float PercentIncrease => percentIncrease;
}
