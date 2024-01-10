using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public event Action<CurrencyType, string> OnCurrencyChanged;
    public List<CurrencyData> currencyDatas = new List<CurrencyData>();

    private void Awake()
    {
    }

    //public void Init()
    //{
    //    // TODO : Save, Load 필요
    //    foreach (CurrencyData currencyData in currencyDatas)
    //    {
    //        OnCurrencyChanged?.Invoke(currencyData.currencyType, currencyData.amount);
    //    }
    //}

    public void AddCurrency(CurrencyType currencyType, BigInteger value)
    {
        CurrencyData data = currencyDatas.Find(c=> c.currencyType == currencyType);
        if(data != null)
        {
            data.Add(value);
            OnCurrencyChanged?.Invoke(currencyType, data.amount); 
            // TODO : Save
        }
    }

    public bool SubtractCurrency(CurrencyType currencyType, BigInteger value)
    {
        CurrencyData data = currencyDatas.Find(c => c.currencyType == currencyType);
        if(data != null)
        {
            bool result = data.Subtract(value);
            // TODO : Save

            if(result)
            {
                OnCurrencyChanged.Invoke(currencyType, data.amount); 
            }
            return result; 
        }
        return false; 
    }

    public string GetCurrencyAmount(CurrencyType type)
    {
        CurrencyData data = currencyDatas.Find(c=>c.currencyType == type);
        return data?.amount ?? "0"; 
    }

    public void ToStringBigInteger(BigInteger biginteger)
    {
        // TODO : 한국 단위로 변경하는 메서드, 유틸로 이동 
    }
}
