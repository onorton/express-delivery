using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        FindObjectOfType<EventBus>().OnScoreUpdated += ScoreUpdated;
    }

    private void ScoreUpdated(int score)
    {
        _text.text = $"{score}";
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnScoreUpdated -= ScoreUpdated;
    }
}
