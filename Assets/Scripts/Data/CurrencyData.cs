using System;
using System.Numerics;

public enum CurrencyType
{
    Gold, Dia
}

[Serializable]
public class CurrencyData
{
    public CurrencyType currencyType;
    public string amount;

    public void Add(BigInteger value)
    {
        BigInteger currentAmount = new BigInteger(int.Parse(amount));
        currentAmount += value;
        amount = currentAmount.ToString();
    }
 
    public bool Subtract(BigInteger value)
    {
        BigInteger currentAmout = new BigInteger(int.Parse(amount));
        if (currentAmout - value < 0) return false;
        currentAmout -= value; 
        amount = currentAmout.ToString();
        return true;
    }

    public CurrencyData(CurrencyType currencyType,string amount)
    {
        this.currencyType = currencyType; 
        this.amount = amount;
    }
}
