using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 10.0f;

    [SerializeField]
    public float BreakingSpeed = 2.0f;

    [SerializeField]
    private float _power;

    [SerializeField]
    private float _maxPower = 10.0f;

    private bool _onGround;

    public bool CarryingPackage;

    private Rigidbody2D _rigidBody;

    private EventBus _eventBus;

    private SpriteRenderer _packageImage;
    private SpriteRenderer _gripperImage;

    private Animator _animator;

    private AudioSource _droneAudioSource;
    private AudioSource _explosionAudioSource;

    private List<Vector2> _currentForces;

    private bool _gameOver = false;

    private float _totalPowerConsumed = 0.0f;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _droneAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource = transform.Find("Collider").GetComponent<AudioSource>();

        CarryingPackage = true;
        _onGround = true;
        _power = _maxPower;
        _eventBus = FindObjectOfType<EventBus>();
        _eventBus.OnReturningHome += ReturnedHome;
        _eventBus.OnGameOver += GameOver;

        _packageImage = transform.Find("Package").GetComponent<SpriteRenderer>();
        _gripperImage = transform.Find("Gripper").GetComponent<SpriteRenderer>();

        _currentForces = new List<Vector2>();

        enabled = false;
    }

    private void Update()
    {
        if (_gameOver)
        {
            _droneAudioSource.Stop();
            _animator.SetBool("Flying", false);
            return;
        }

        _eventBus.PowerUpdateEvent(_power / _maxPower);
        _eventBus.TotalPowerUpdateEvent(_totalPowerConsumed);

        if (_power <= 0.0f && _onGround)
        {
            _eventBus.GameOverEvent(GameOverReason.OutOfPower);
        }

        var currentlyRunning = _currentForces.Count > 0;

        _currentForces = new List<Vector2>();

        if (Input.GetKey(KeyCode.UpArrow) && _power > 0.0f)
        {
            var powerConsumed = Mathf.Min(_power, Time.deltaTime);
            _power -= powerConsumed;
            _currentForces.Add(_rigidBody.mass * (_acceleration + Physics2D.gravity.magnitude) * Vector2.up);
            _totalPowerConsumed += powerConsumed;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !_onGround && _power > 0.0f)
        {
            var powerConsumed = Mathf.Min(_power, Time.deltaTime);
            _power -= Mathf.Min(_power, Time.deltaTime);
            _currentForces.Add(_rigidBody.mass * _acceleration * Vector2.left);
            _totalPowerConsumed += powerConsumed;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !_onGround && _power > 0.0f)
        {
            var powerConsumed = Mathf.Min(_power, Time.deltaTime);
            _power -= Mathf.Min(_power, Time.deltaTime);
            _currentForces.Add(_rigidBody.mass * _acceleration * Vector2.right);
            _totalPowerConsumed += powerConsumed;
        }

        if (_currentForces.Count > 0 & !currentlyRunning)
        {
            _droneAudioSource.Play();
        }
        else if (_currentForces.Count == 0)
        {
            _droneAudioSource.Stop();
        }
        _animator.SetBool("Flying", _currentForces.Count > 0);
        _eventBus.SpeedUpdateEvent(_rigidBody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        foreach (var force in _currentForces)
        {
            _rigidBody.AddForce(force);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _onGround = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _onGround = false;
        }

    }

    private void ReturnedHome(bool carryingPackage)
    {
        if (_gameOver)
        {
            return;
        }

        if (!carryingPackage)
        {
            CarryingPackage = true;
            _packageImage.enabled = true;
            _gripperImage.enabled = true;
        }
        _power = _maxPower;
    }

    public void RemovePackage()
    {
        if (CarryingPackage)
        {
            CarryingPackage = false;
            _packageImage.enabled = false;
            _gripperImage.enabled = false;
        }
    }

    private void GameOver(GameOverReason reason)
    {
        if (reason == GameOverReason.DroneDestroyed)
        {
            _animator.SetBool("Destroyed", true);
            _explosionAudioSource.Play();
        }
        _gameOver = true;
        _currentForces.Clear();
    }

    public void SetGameOver()
    {
        _gameOver = true;
    }

    public bool IsExceedingBreakingSpeed()
    {
        return _rigidBody.velocity.magnitude > BreakingSpeed;
    }

    public bool IsStationary()
    {
        return _rigidBody.velocity.magnitude <= 0.0001f;
    }

    private void OnDestroy()
    {
        _eventBus.OnReturningHome -= ReturnedHome;
    }
}
