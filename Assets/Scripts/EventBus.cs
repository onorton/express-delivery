using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public delegate void ReachedTarget();
    public event ReachedTarget OnReachingTarget;

    public delegate void ReturnedHome(bool carryingPackage);
    public event ReturnedHome OnReturningHome;

    public delegate void ScoreUpdate(int score);
    public event ScoreUpdate OnScoreUpdated;


    public delegate void StatUpdated(float stat);
    public event StatUpdated OnSpeedUpdated;
    public event StatUpdated OnPowerUpdated;
    public event StatUpdated OnTotalPowerUpdated;

    public delegate void GameOver(GameOverReason reason);
    public event GameOver OnGameOver;


    public delegate void NewTarget(Target target);
    public event NewTarget OnNewTarget;


    public void ReturningHomeEvent(bool carryingPackage)
    {
        OnReturningHome.Invoke(carryingPackage);
    }

    public void ReachingTargetEvent()
    {
        OnReachingTarget.Invoke();
    }

    public void ScoreUpdateEvent(int score)
    {
        OnScoreUpdated.Invoke(score);
    }

    public void SpeedUpdateEvent(float speed)
    {
        OnSpeedUpdated.Invoke(speed);
    }

    public void PowerUpdateEvent(float power)
    {
        OnPowerUpdated.Invoke(power);
    }

    public void TotalPowerUpdateEvent(float power)
    {
        OnTotalPowerUpdated.Invoke(power);
    }

    public void GameOverEvent(GameOverReason reason)
    {
        OnGameOver.Invoke(reason);
    }

    public void NewTargetEvent(Target target)
    {
        OnNewTarget.Invoke(target);
    }

    public bool EventHandlersAreEnabled()
    {
        return OnReachingTarget != null && OnReturningHome != null && OnScoreUpdated != null && OnGameOver != null && OnNewTarget != null;
    }
}


public enum GameOverReason
{
    DroneDestroyed,
    OutOfPower
}
