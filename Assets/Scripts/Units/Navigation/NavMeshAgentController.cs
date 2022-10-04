using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float basicSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float updateDestinationInterval;

    private Unit unitTarget;
    
    private float updateDestinationTime;

    public Vector3 Velocity => agent.velocity;

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && agent.hasPath)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
            }
        }
    }

    private void Start()
    {
        updateDestinationTime = updateDestinationInterval;
    }

    private void FixedUpdate()
    {
        agent.speed = basicSpeed * Time.timeScale;
        updateDestinationTime -= Time.fixedDeltaTime;

        if (updateDestinationTime <= 0)
        {
            updateDestinationTime = updateDestinationInterval;
            UpdateDestination();
        }

        Rotate();
    }

    private void UpdateDestination()
    {
        if (unitTarget != null)
            agent.SetDestination(unitTarget.transform.position);
        else
            agent.SetDestination(transform.position);
    }

    private void Rotate()
    {
        Vector3 directionToTarget = transform.forward;

        if (agent.hasPath)
        {
            var corners = agent.path.corners;
            int pathLength = corners.Length;

            if (pathLength > 2)
            {
                directionToTarget = (corners[1] - transform.position);
            }
            else
            if (unitTarget != null && unitTarget.gameObject.activeSelf)
            {
                directionToTarget = (unitTarget.transform.position - transform.position);
            }
        }
        else
        if (unitTarget != null && unitTarget.gameObject.activeSelf)
        {
            directionToTarget = (unitTarget.transform.position - transform.position);
        }

        directionToTarget.y = transform.forward.y;

        if (directionToTarget.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionToTarget.normalized), rotateSpeed);
    }

    public void SetEntityTarget(Unit target)
    {
        this.unitTarget = target;
    }

    public void ClearTarget()
    {
        this.unitTarget = null;
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (agent != null)
                basicSpeed = agent.speed;
        }
    }
}
