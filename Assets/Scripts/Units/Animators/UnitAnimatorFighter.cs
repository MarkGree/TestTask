using UnityEngine;

public class UnitAnimatorFighter : UnitAnimator
{
    [SerializeField] protected Unit myUnit;
    [SerializeField] private float fightRange;

    [SerializeField] protected string anim_FightName;

    protected bool isFighting;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var fightEntity = myUnit.Targeting.TargetUnit;

        if (fightEntity != null)
        {
            float distance = PlaneVectors.Distance(fightEntity.transform.position, transform.position);

            if (!isFighting)
            {
                if (distance < fightRange)
                {
                    isFighting = true;
                }
            }
            else
            {
                if (distance >= fightRange)
                {
                    isFighting = false;
                }
            }
        }
        else
        {
            isFighting = false;
        }

        animator.SetBool(anim_FightName, isFighting);
    }
}
