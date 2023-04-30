using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointToTarget : MonoBehaviour
{

    private Camera _camera;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _drone;

    private RectTransform _pointerTransform;

    private Image _image;

    private Vector2 _sizeDelta;


    private void Start()
    {
        _camera = Camera.main;
        _pointerTransform = GetComponent<RectTransform>();
        FindObjectOfType<EventBus>().OnNewTarget += NewTargetPicked;
        FindObjectOfType<EventBus>().OnReachingTarget += ReachedTarget;
        _image = GetComponent<Image>();
        _sizeDelta = _pointerTransform.sizeDelta;
    }

    private void NewTargetPicked(Target target)
    {
        _target = target.transform;
        _image.enabled = true;
    }

    private void ReachedTarget()
    {
        _image.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        var viewportPoint = _camera.WorldToViewportPoint(_target.position);
        if (viewportPoint.x <= 0.95f && viewportPoint.x >= 0.05 && viewportPoint.y <= 0.95f && viewportPoint.y >= 0.05)
        {
            _pointerTransform.position = _camera.WorldToScreenPoint(_target.position + new Vector3(0.0f, 2.0f, 0.0f));
            _pointerTransform.rotation = Quaternion.FromToRotation(Vector2.up, Vector2.down);
            _pointerTransform.sizeDelta = 0.5f * _sizeDelta;
        }
        else
        {
            var worldPoint = _camera.ScreenToWorldPoint(_pointerTransform.position);
            _pointerTransform.rotation = Quaternion.FromToRotation(Vector2.up, new Vector2(_target.position.x, _target.position.y) - new Vector2(worldPoint.x, worldPoint.y));
            _pointerTransform.sizeDelta = _sizeDelta;
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<EventBus>().OnNewTarget -= NewTargetPicked;
        FindObjectOfType<EventBus>().OnReachingTarget -= ReachedTarget;
    }
}
