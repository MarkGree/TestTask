using UnityEngine;

public class UnitDebugFighterParamsGetter : UnitDebugParametersGetter
{
    [SerializeField] protected Vector2 damageRange;

    public override Unit.UnitParameters Get()
    {
        return new UnitFighter.UnitParametersFighter(healthRange.GetRandom(), damageRange.GetRandom());
    }
}
