using UnityEngine;

public abstract class UnitDebugParametersGetter : MonoBehaviour
{
    [SerializeField] protected Vector2 healthRange;

    public abstract Unit.UnitParameters Get();
}
