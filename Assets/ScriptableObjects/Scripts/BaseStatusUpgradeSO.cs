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

    public StatusType StatusType { get; private set; }
    public int CurrentUpgradeLevel { get; private set; }
    public int MaxUpgradeLevel { get; private set; }
    public int UpgradePrice { get; private set; }
    public int PricePercent { get; private set; }
    public BigInteger UpgradeValue { get; private set; }
    public int Increase { get; private set; }
    public float PercentUpgradeValue { get; private set; }
    public float PercentIncrease { get; private set; }
}
