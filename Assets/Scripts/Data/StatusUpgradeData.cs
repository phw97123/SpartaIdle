using System;
using System.Diagnostics;
using System.Numerics;

public enum StatusType { Atk, Hp, Crit_ch, Crit_Dmg, Hp_Re, Atk_Big, Hp_Big, Mp, Mp_Re }

public enum DataType { Int, Float }
public class StatusUpgradeData
{
    public BaseStatusUpgradeSO baseSo;

    public int currentUpgradeLevel;
    public int maxUpgradeLevel;

    public int upgradePrice;
    public int pricePercent;

    public Action<StatusType, int> OnStatusUpgrade;
    public BigInteger upgradeValue;
    public int increase;

    public Action<StatusType, float> OnPercentUpgrade;
    public float percentUpgradeValue;
    public float percentIncrease;

    public StatusUpgradeData(BaseStatusUpgradeSO baseSo)
    {
        this.baseSo = baseSo;
        currentUpgradeLevel = baseSo.CurrentUpgradeLevel;
        maxUpgradeLevel = baseSo.MaxUpgradeLevel;
        upgradePrice = baseSo.UpgradePrice;
        pricePercent = baseSo.PricePercent;

        upgradeValue = baseSo.UpgradeValue;
        increase = baseSo.Increase;

        percentUpgradeValue = baseSo.PercentUpgradeValue;
        percentIncrease = baseSo.PercentIncrease;
    }

    public void UpgradeUpdate()
    {
        switch (baseSo.dataType)
        {
            case DataType.Int:
                StatusUpdate();
                break;
            case DataType.Float:
                PercentStatusUpdate();
                break;
        }
    }

    private void StatusUpdate()
    {
        if(currentUpgradeLevel < maxUpgradeLevel)
        {
            currentUpgradeLevel++;
            upgradeValue += increase;
            upgradePrice += pricePercent;
            pricePercent += pricePercent;

            // TODO : Save
            OnStatusUpgrade?.Invoke(baseSo.StatusType, increase);
        }
    }

    private void PercentStatusUpdate()
    {
        currentUpgradeLevel++;
        percentUpgradeValue += percentIncrease;
        upgradePrice += pricePercent;
        pricePercent += pricePercent; 
        // TODO : Save
        OnPercentUpgrade?.Invoke(baseSo.StatusType, percentUpgradeValue);
    }
}
