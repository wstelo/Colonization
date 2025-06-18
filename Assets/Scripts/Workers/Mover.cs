using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    private NavMeshAgent _agent;

    public bool HasActivePath => _agent.remainingDistance > _agent.stoppingDistance;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
        
    public void MoveToPoint(Vector3 point)
    {
        _agent.isStopped = false;

        if (IsDestinationReachable(point))
        {
            _agent.SetDestination(point);
        }
    }

    public void StopMovement()
    {
        _agent.isStopped = true;
    }

    private bool IsDestinationReachable(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();

        if (_agent.CalculatePath(target, path))
        {
            return path.status == NavMeshPathStatus.PathComplete;
        }

        return false;
    }
}
