using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesDrone : MonoBehaviour
{
    private EventBus _eventBus;
    [SerializeField]
    private bool alwaysDamages;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Drone>() != null && (alwaysDamages || other.relativeVelocity.magnitude > other.collider.GetComponent<Drone>().BreakingSpeed))
        {
            other.collider.GetComponent<Drone>().SetGameOver();
            _eventBus.GameOverEvent(GameOverReason.DroneDestroyed);
        }
    }
}
