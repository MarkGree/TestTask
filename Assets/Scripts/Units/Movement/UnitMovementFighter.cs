public class UnitMovementFighter : UnitMovement
{
    private UnitFighter myUnitFighter;

    private void Awake()
    {
        myUnitFighter = unit as UnitFighter;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var fightUnit = myUnitFighter.Targeting.TargetUnit;

        if (fightUnit != null)
        {
            agent.SetEntityTarget(fightUnit);
        }

        SetIsMoving();
    }
}
