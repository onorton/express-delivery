using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private EventBus _eventBus;
    private AudioManager _audioManager;

    [SerializeField]
    private AudioClip _dropOffAudioClip;

    private void Start()
    {
        _eventBus = FindObjectOfType<EventBus>();
        _audioManager = FindObjectOfType<AudioManager>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Drone>() != null && other.GetComponent<Drone>().IsStationary() && other.GetComponent<Drone>().CarryingPackage)
        {
            _eventBus.ReachingTargetEvent();
            _audioManager.Play(_dropOffAudioClip);
        }
    }
}
