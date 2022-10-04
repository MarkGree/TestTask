using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] protected Unit unit;
    [SerializeField] protected NavMeshAgentController agent;

    private const float isMovingThreshold = 0.1f;
    
    
    protected Unit target;

    protected bool isMoving;
    public bool IsMoving => isMoving;


    protected virtual void FixedUpdate()
    {
        agent.SetEntityTarget(unit.Targeting.TargetUnit);

        SetIsMoving();
    }

    protected void SetIsMoving()
    {
        isMoving = agent.Velocity.sqrMagnitude > isMovingThreshold;
    }
}
