using System.Numerics;
using UnityEngine;

[CreateAssetMenu()]
public class BaseStatusUpgradeSO : ScriptableObject
{
    [SerializeField] private StatusType statusType;
    [SerializeField] public DataType dataType;
    [SerializeField] public CurrencyType currencyType;

    [SerializeField] public string upgradeName; 

    [SerializeField] private int currentUpgradeLevel;
    [SerializeField] private int maxUpgradeLevel;

    [SerializeField] private int upgradePrice;
    [SerializeField] private int pricePercent;

    [SerializeField] private BigInteger upgradeValue;
    [SerializeField] private int increase;

    [SerializeField] private float percentUpgradeValue;
    [SerializeField] private float percentIncrease;

    public StatusType StatusType => statusType; 
    public int CurrentUpgradeLevel => currentUpgradeLevel;
    public int MaxUpgradeLevel => maxUpgradeLevel;
    public int UpgradePrice => upgradePrice;
    public int PricePercent => pricePercent;
    public BigInteger UpgradeValue => upgradeValue;
    public int Increase => increase;
    public float PercentUpgradeValue => percentUpgradeValue;
    public float PercentIncrease => percentIncrease;
}
