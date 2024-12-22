using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateState(bool isGrounded, float verticalVelocity, bool isMoving, bool isAlive)
    {
        if (!isAlive)
        {
            TriggerDeathAnimation();
            return;
        }

        if (isGrounded)
        {
            animator.SetInteger("state", isMoving ? (int)PlayerState.Running : (int)PlayerState.Idle);
        }
        else
        {
            animator.SetInteger("state", verticalVelocity > 0 ? (int)PlayerState.Jumping : (int)PlayerState.Falling);
        }
    }

    public void TriggerDeathAnimation()
    {
        animator.SetInteger("state", (int)PlayerState.Dying);
    }
}