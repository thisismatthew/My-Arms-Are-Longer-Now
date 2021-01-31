using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debt : MonoBehaviour
{
    private int debtValue = 52000;
    public Text debtText;

    public int DebtValue { get => debtValue; set => debtValue = value; }

    // Update is called once per frame
    void Update()
    {
        debtText.text = "$" + DebtValue;
    }
}
