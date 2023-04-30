using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private EventBus _eventBus;
    private AudioManager _audioManager;

    [SerializeField]
    private AudioClip _pickupAudioClip;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
        _audioManager = FindObjectOfType<AudioManager>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Drone>() != null && other.GetComponent<Drone>().IsStationary())
        {
            if (!other.GetComponent<Drone>().CarryingPackage)
            {
                _audioManager.Play(_pickupAudioClip);
            }
            _eventBus.ReturningHomeEvent(other.GetComponent<Drone>().CarryingPackage);
        }
    }
}
