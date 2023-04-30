using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    private float _originalCameraX;

    private float _originalX;

    [SerializeField]
    private float _parallax;

    void Start()
    {
        _originalCameraX = _camera.transform.position.x;
    }

    void LateUpdate()
    {
        var new_x = _originalX + (_camera.transform.position.x - _originalCameraX) * _parallax;
        var destination = new Vector3(new_x, transform.position.y, transform.position.z);

        transform.position = destination;
    }
}
