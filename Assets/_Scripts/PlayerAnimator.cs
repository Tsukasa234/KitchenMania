using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Update()
    {
        m_Animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
