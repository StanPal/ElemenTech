using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private bool _IsInvincible = false;
    public bool DashProjectileInvincibility { get => _IsInvincible; set => _IsInvincible = value; }

    private void InvincibleOn()
    {
        _IsInvincible = true;
    }

    private void InvincibleOff()
    {
        _IsInvincible = false;
    }
}
