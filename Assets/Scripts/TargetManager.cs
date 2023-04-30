using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private List<Target> _targets;
    private EventBus _eventBus;

    // Start is called before the first frame update
    private void Start()
    {
        _targets = GameObject.Find("Targets").GetComponentsInChildren<Target>().ToList();
        _eventBus = FindObjectOfType<EventBus>();

        DisableTargets();
        PickNewTarget();
    }

    public void PickNewTarget()
    {
        var inactiveTargets = _targets.Where(target => !target.enabled).ToList();
        DisableTargets();
        var newTarget = inactiveTargets[new System.Random().Next() % inactiveTargets.Count()];
        newTarget.enabled = true;
        StartCoroutine(CreateNewTargetEvent(newTarget));
    }

    private IEnumerator CreateNewTargetEvent(Target target)
    {
        while (true)
        {
            if (_eventBus.EventHandlersAreEnabled())
            {
                _eventBus.NewTargetEvent(target);
                break;
            }
            yield return new WaitForSeconds(.1f);
        }

    }

    private void DisableTargets()
    {
        foreach (var target in _targets)
        {
            target.enabled = false;
        }
    }
}
