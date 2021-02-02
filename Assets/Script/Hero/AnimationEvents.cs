using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private bool _IsInvincible = false;
    public bool DashProjectileInvincibility { get { return _IsInvincible; } set { _IsInvincible = value; } }
}
