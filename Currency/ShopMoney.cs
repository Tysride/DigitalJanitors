using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMoney : MonoBehaviour
{

    public Text MoneyText;

    void Update()
    {
        MoneyText.text = "" + DataManager.Instance.currency;
    }
}
