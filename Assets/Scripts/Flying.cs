using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Vector2 Direction;

    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = _speed * Direction;
        GetComponent<Animator>().SetFloat("Direction", _rigidBody.velocity.x);
    }
}
