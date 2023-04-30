using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        FindObjectOfType<EventBus>().OnPowerUpdated += PowerUpdated;
    }

    private void PowerUpdated(float Power)
    {
        _slider.value = Power;
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnPowerUpdated -= PowerUpdated;
    }
}
