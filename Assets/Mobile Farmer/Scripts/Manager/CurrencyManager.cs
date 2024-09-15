using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private int currency;
    private const string CURRENCY = "currency";

    private void Awake()
    {
        LoadCurrency();
    }

    public void ChangeCurrency(int amount)
    {
        currency += amount;
        currency = Mathf.Clamp(currency, 0, currency);
        SaveCurrency();
    }

    public void UpdateDisplayCurrency()
    {
        currencyText.text = currency.ToString();
    }

    public void LoadCurrency()
    {
        currency = ES3.Load<int>(CURRENCY, 0);
        UpdateDisplayCurrency();
    }

    public void SaveCurrency()
    {
        ES3.Save<int>(CURRENCY, currency);
        UpdateDisplayCurrency();
    }

}
