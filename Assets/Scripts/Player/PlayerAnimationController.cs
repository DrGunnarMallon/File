using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #region Animation Logic

    public void SetState(PlayerState state)
    {
        animator.SetInteger("state", (int)state);
    }

    public void UpdateAnimation(bool isAlive, bool isGrounded, float verticalVelocity, bool hasHorizontalSpeed)
    {
        if (!isAlive)
        {
            SetState(PlayerState.Dying);
            return;
        }

        SetState(PlayerState.Idle);

        if (isGrounded)
        {
            if (hasHorizontalSpeed)
            {
                SetState(PlayerState.Running);
            }
            else
            {
                SetState(PlayerState.Idle);
            }
        }
        else
        {
            if (verticalVelocity > 0)
            {
                SetState(PlayerState.Jumping);
            }
            else
            {
                SetState(PlayerState.Falling);
            }
        }
    }

    #endregion
}
