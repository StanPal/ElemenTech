using UnityEngine;

public class WaterSkills : MonoBehaviour
{
    public GameObject WaterGun;
    PlayerSkills _HeroSkills;

    [SerializeField] private float _Speed = 10f;
    [SerializeField] private float _Damage = 5f;
    //After a certain duration of time destroy gameobject if it is active
    [SerializeField] private float _ExitTime;
    [SerializeField] private float _SlowAmount = 1f;
    [SerializeField] private float _SlowDuration = 1f;

    // Getters
    public PlayerSkills PlayerSkills { get { return _HeroSkills; } }
    public float Speed { get { return _Speed; } }
    public float Damage { get { return _Damage; } }
    public float ExitTime { get { return _ExitTime; } }
    public float SlowAmount { get { return _SlowAmount; } }
    public float SlowDuration { get { return _SlowAmount; } }


    private void Start()
    {
        _HeroSkills = GetComponent<PlayerSkills>();
        _HeroSkills.onWaterSkillPerformed += WaterGunCast;
    }

    void WaterGunCast()
    {
        Instantiate(WaterGun, _HeroSkills.HeroAction.FirePoint.transform.position, Quaternion.Euler(0, 0, _HeroSkills.HeroAction.GetLookAngle));
    }
}
