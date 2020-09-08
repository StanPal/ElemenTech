using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField]
    float defDistanceRany = 100;
    public LineRenderer mLineRender;
    Transform mTransform;

    private void Awake()
    {
        mTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        if(Physics2D.Raycast(mTransform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(mTransform.position, transform.right);
            Draw2DRay(gameObject.transform.position, gameObject.transform.right * defDistanceRany);
        }
    }

    void Draw2DRay(Vector2 starPos, Vector2 endPos)
    {
        mLineRender.SetPosition(0, starPos);
        mLineRender.SetPosition(1, endPos);
    }
}
