using UnityEngine;

public class Pivot : MonoBehaviour
{
    private HeroActions _HeroActions;

    private void Awake()
    {
        _HeroActions = GetComponentInParent<HeroActions>();
    }

    // Update is called once per frame
    void Update()
    {   
         transform.rotation = Quaternion.Euler(0.0f, 0.0f, _HeroActions.GetLookAngle);
    }
}
