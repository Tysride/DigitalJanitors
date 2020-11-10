using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public TextMeshPro MoneyText;

    #region Singleton
    public static Money instance;

    private void Start()
    {
        CurrencyChanged();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            MoneyText = GetComponent<TextMeshPro>();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion

    public void CurrencyChanged()
    {
        MoneyText.text = "" + DataManager.Instance.currency;
    }
}
