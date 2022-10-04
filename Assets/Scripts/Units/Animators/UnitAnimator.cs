using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Unit character;
    [SerializeField] protected Animator animator;

    [SerializeField] protected string anim_RunningName;

    protected virtual void FixedUpdate()
    {
        animator.SetBool(anim_RunningName, character.Movement.IsMoving);
    }
}
