using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{
    [SerializeField] protected UnitFighter unit;

    public abstract void Attack();
}
