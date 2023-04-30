using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private TextMeshProUGUI _reasonText;

    private void Start()
    {
        _reasonText = transform.Find("Reason").GetComponent<TextMeshProUGUI>();
        FindObjectOfType<EventBus>().OnGameOver += GameOver;
        gameObject.SetActive(false);
    }

    private void GameOver(GameOverReason reason)
    {
        switch (reason)
        {
            case GameOverReason.DroneDestroyed:
                _reasonText.text = "Drone destroyed."; break;
            case GameOverReason.OutOfPower:
                _reasonText.text = "Out of power"; break;
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnGameOver -= GameOver;
    }
}
