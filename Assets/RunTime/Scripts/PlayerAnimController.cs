using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationConstants
{
    public const string VelocityY = "VelocityY";
}

public class PlayerAnimController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        animator.SetFloat(PlayerAnimationConstants.VelocityY, player.Velocity.y);
    }

    public void Die()
    {
        animator.enabled = false;
        enabled = false;
    }
}
