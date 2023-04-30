using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<EventBus>().OnSpeedUpdated += SpeedUpdated;
    }

    private void SpeedUpdated(float speed)
    {
        _text.text = $"{speed.ToString("0.00")} m/s";
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnSpeedUpdated -= SpeedUpdated;
    }
}
