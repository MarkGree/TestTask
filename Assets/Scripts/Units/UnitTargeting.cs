using UnityEngine;

public class UnitTargeting : MonoBehaviour
{
    [SerializeField] private Unit myUnit;
    
    private Unit targetUnit;
    public Unit TargetUnit => targetUnit;

    private void OnEnable()
    {
        myUnit.Parameters.OnDie += ClearTarget;
    }

    public void SetTarget(Unit unit)
    {
        targetUnit = unit;

        targetUnit.Parameters.OnDie += ClearTarget;
    }

    private void ClearTarget()
    {
        if (targetUnit != null)
        {
            targetUnit.Parameters.OnDie -= ClearTarget;

            targetUnit = null;
        }
    }
}
