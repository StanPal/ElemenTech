using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]  private bool _IsInvincible = false;
    public bool DashProjectileInvincibility { get => _IsInvincible; set => _IsInvincible = value; }

    public void InvincibleOn()
    {
        _IsInvincible = true;
    }

    public void InvincibleOff()
    {
        _IsInvincible = false;
    }

}
