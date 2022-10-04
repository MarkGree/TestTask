using UnityEngine;

public class UnitAttackMelee : UnitAttack
{
    [SerializeField] private float attackRange;

    public override void Attack()
    {
        var target = unit.Targeting.TargetUnit;

        
        if (target != null && IsInAttackRange(target) && IsFacingTarget(target))
        {
            //Debug.Log("DoAttack: " + target.)
            target.Hit(unit.Damage);
        }
    }

    protected bool IsInAttackRange(Unit target)
    {
        float distance = PlaneVectors.Distance(unit.transform.position, target.transform.position);

        return distance <= attackRange;
    }

    protected bool IsFacingTarget(Unit target)
    {
        if (target == null) return false;

        var enemyPos = target.transform.position;
        enemyPos.y = transform.position.y;

        var targetDirection = enemyPos - transform.position;

        float dot = Vector3.Dot(targetDirection.normalized, transform.forward);

        return dot > 0.5f;
    }
}
