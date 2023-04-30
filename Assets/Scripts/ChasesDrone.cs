using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasesDrone : MonoBehaviour
{

    [SerializeField]
    private Bounds _range;
    private Rigidbody2D _rigidBody;
    private Transform _target;

    private Vector3? _targetPosition;
    private bool _chasingTarget;

    [SerializeField]
    private float _chaseDistance = 5.0f;

    [SerializeField]
    private float _speed;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _range.center = transform.position;
        _chasingTarget = false;
    }

    void FixedUpdate()
    {
        if (_range.Contains(_target.position) || (_target.position - transform.position).magnitude < _chaseDistance)
        {
            var hit = Physics2D.Raycast(transform.position, (_target.position - transform.position).normalized, _chaseDistance * 2, LayerMask.GetMask("Player", "Default"));
            if (hit.collider.gameObject == _target.gameObject)
            {

                _targetPosition = _target.position;
                _chasingTarget = true;
            }
            else
            {
                _chasingTarget = false;
            }
        }
        else
        {
            _chasingTarget = false;
        }


        if (!_chasingTarget)
        {

            if (_targetPosition != null && Vector3.Distance(_targetPosition.Value, transform.position) < 0.1f)
            {
                _targetPosition = null;
            }

            if (_targetPosition == null)
            {
                _targetPosition = RandomPositionInBounds();
            }
        }

        var direction = (_targetPosition.Value - transform.position).normalized;
        _rigidBody.velocity = direction * _speed;
        _animator.SetFloat("Direction", _rigidBody.velocity.x);
    }

    private Vector2 RandomPositionInBounds()
    {
        var random = new System.Random();
        var xOffset = random.NextDouble() * 2.0 - 1.0;
        var yOffset = random.NextDouble() * 2.0 - 1.0;
        return new Vector2((float)(_range.center.x + xOffset * _range.extents.x), (float)(_range.center.y + yOffset * _range.extents.y));
    }
}
