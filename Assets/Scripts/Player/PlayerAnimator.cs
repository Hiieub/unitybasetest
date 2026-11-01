using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int InteractHash = Animator.StringToHash("Interact");
    private static readonly int DeathHash = Animator.StringToHash("Death");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetRunning(bool isRunning) => animator.SetBool(IsRunningHash, isRunning);
    public void SetGrounded(bool isGrounded) => animator.SetBool(IsGroundedHash, isGrounded);
    public void PlayJump() => animator.SetTrigger(JumpHash);
    public void PlayInteract() => animator.SetTrigger(InteractHash);
    public void PlayDeath() => animator.SetTrigger(DeathHash);
}
