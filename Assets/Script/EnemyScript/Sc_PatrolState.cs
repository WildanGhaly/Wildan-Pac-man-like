using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {
        enemy.Animator.SetTrigger("PatrolState");
        _isMoving = false;
        SetNextDestination(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
            return;
        }

        if (!_isMoving)
        {
            SetNextDestination(enemy);
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, _destination) <= 0.5f)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }

    private void SetNextDestination(Enemy enemy)
    {
        if (enemy.Waypoints.Count == 0) return;

        _isMoving = true;
        int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
        _destination = enemy.Waypoints[index].position;

        enemy.NavMeshAgent.SetDestination(_destination);

        Debug.Log("Moving to waypoint: " + _destination);
    }
}
