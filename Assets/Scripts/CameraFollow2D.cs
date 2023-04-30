using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Vector3 _previousPosition;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;

    // yOffset of camera from target
    private float yOffset = 0.5f;

    // In viewport space
    [SerializeField]
    private float _horizontalPadding = 0.3f;


    [SerializeField]
    private float _verticalPadding = 0.3f;

    // Non-grid-aligned position
    private Vector3 _unadjustedPosition;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        transform.position = new Vector3(_target.position.x, _target.position.y + yOffset, transform.position.z);
        _unadjustedPosition = transform.position;
    }

    private void LateUpdate()
    {
        var point = _camera.WorldToViewportPoint(_target.position);


        var delta_x = 0f;
        var delta_y = 0f;

        if (point.x > 1.0f - _horizontalPadding)
        {
            delta_x = point.x - (1 - _horizontalPadding);

        }
        else if (point.x < _horizontalPadding)
        {
            delta_x = point.x - _horizontalPadding;
        }

        if (point.y > 1.0f - _verticalPadding)
        {
            delta_y = point.y - (1 - _verticalPadding);

        }
        else if (point.y < _verticalPadding)
        {
            delta_y = point.y - _verticalPadding;
        }


        var destination = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f) + new Vector3(delta_x, delta_y, 0.0f));

        _unadjustedPosition = Vector3.SmoothDamp(_unadjustedPosition, destination, ref _velocity, 0.3f);


        transform.position = _unadjustedPosition;
    }
}
