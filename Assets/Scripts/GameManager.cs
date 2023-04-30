using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private EventBus _eventBus;
    private int _score;
    private GameObject _gameOverUI;
    private TargetManager _targetManager;

    private bool _gameOver = false;


    private void Awake()
    {
        _gameOverUI = GameObject.Find("UI/GameOver").gameObject;

    }

    private void Start()
    {
        _score = 0;
        _eventBus = FindObjectOfType<EventBus>();
        _eventBus.OnReachingTarget += PlayerReachedTarget;
        _eventBus.OnReturningHome += PlayerReturnedHome;
        _eventBus.OnGameOver += GameOver;


        _targetManager = FindObjectOfType<TargetManager>();
    }

    private void PlayerReachedTarget()
    {
        var drone = GameObject.FindGameObjectWithTag("Player").GetComponent<Drone>();
        if (drone.CarryingPackage)
        {
            drone.RemovePackage();
            _score += 1;
            _eventBus.ScoreUpdateEvent(_score);
        }
    }

    private void PlayerReturnedHome(bool carryingPackage)
    {
        if (_gameOver)
        {
            return;
        }

        if (!carryingPackage)
        {
            _targetManager.PickNewTarget();
        }
    }

    private void GameOver(GameOverReason gameOverReason)
    {
        _gameOverUI.SetActive(true);
        _gameOver = true;
    }

    private void OnDestroy()
    {
        _eventBus.OnReachingTarget -= PlayerReachedTarget;
        _eventBus.OnReturningHome -= PlayerReturnedHome;
        _eventBus.OnGameOver -= GameOver;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
