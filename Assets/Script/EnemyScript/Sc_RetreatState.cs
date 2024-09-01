using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Retreating");
        enemy.Animator.SetTrigger("RetreatState");
        SetRetreatDestination(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            if (enemy.NavMeshAgent.remainingDistance <= enemy.NavMeshAgent.stoppingDistance)
            {
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Retreating");
    }

    private void SetRetreatDestination(Enemy enemy)
    {
        if (enemy.Player == null) return;

        Vector3 directionAwayFromPlayer = enemy.transform.position - enemy.Player.transform.position;
        directionAwayFromPlayer.Normalize();

        Vector3 retreatPosition = enemy.transform.position + directionAwayFromPlayer * 10f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatPosition, out hit, 10f, NavMesh.AllAreas))
        {
            enemy.NavMeshAgent.SetDestination(hit.position);

            Debug.Log("Retreating to: " + hit.position);
        }
        else
        {
            retreatPosition = enemy.transform.position + directionAwayFromPlayer * 20f;

            if (NavMesh.SamplePosition(retreatPosition, out hit, 10f, NavMesh.AllAreas))
            {
                enemy.NavMeshAgent.SetDestination(hit.position);
                Debug.Log("Fallback retreating to: " + hit.position);
            }
        }
    }
}
