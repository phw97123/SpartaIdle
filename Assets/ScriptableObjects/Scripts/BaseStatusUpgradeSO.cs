using System.Numerics;
using UnityEngine;

[CreateAssetMenu()]
public class BaseStatusUpgradeSO : ScriptableObject
{
    [SerializeField] private StatusType statusType;
    [SerializeField] public DataType dataType;
    [SerializeField] public CurrencyType currencyType;

    [Header("[능력치 이름]")]
    [SerializeField] public string upgradeName;

    [Header("[레벨, 최대 레벨]")]
    [SerializeField] private int baseUpgradeLevel;
    [SerializeField] private int maxUpgradeLevel;

    [Header("[초기 가격, 가격 증가율]")]
    [SerializeField] private int baseUpgradePrice;
    [SerializeField] private int pricePercent;

    [Header("[int 초기 값, 증가량]")]
    [SerializeField] private int upgradeValue;
    [SerializeField] private int increase;

    [Header("[float 초기 값, 증가량]")]
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
