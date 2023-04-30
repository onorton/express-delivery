using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalPowerUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<EventBus>().OnTotalPowerUpdated += TotalPowerUpdated;
    }

    private void TotalPowerUpdated(float totalPowerConsumed)
    {
        _text.text = $"{totalPowerConsumed.ToString("0.00")} Wh";
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnTotalPowerUpdated -= TotalPowerUpdated;
    }
}
