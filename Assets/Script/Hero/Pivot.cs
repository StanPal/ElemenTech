using UnityEngine;

public class Pivot : MonoBehaviour
{
    private HeroActions _heroActions;

    private void Awake()
    {
        _heroActions = GetComponentInParent<HeroActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_heroActions.HeroMovement == null)
        {
            return;
        }

        if (_heroActions.HeroMovement.GetIsLeft)
        {
            Vector3 objectscale = transform.localScale;
            objectscale.x = -0.1f;
            transform.localScale = objectscale;
        }
        else
        {
            Vector3 objectscale = transform.localScale;
            objectscale.x = 0.1f;
            transform.localScale = objectscale;
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _heroActions.GetLookAngle);

    }
}
