using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public event Action<CurrencyType, string> OnCurrencyChanged;
    public List<CurrencyData> currencyDatas= new List<CurrencyData>();

    private void Awake()
    {
        instance = this; 
    }

    // TODO : Init

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

    public void UpdateCurrencyUI(CurrencyType type, string amount)
    {
        CurrencyData data = currencyDatas.Find(c => c.currencyType == type); 
        //currencyDatas.
    }
}
