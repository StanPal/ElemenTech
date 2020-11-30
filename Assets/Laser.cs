using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer mLineRenderer;
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
            RaycastHit2D hit = Physics2D.Raycast(mTransform.position, transform.right);
            Draw2DRay(laserFirePoint.position, hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        mLineRenderer.SetPosition(0, startPos);
        mLineRenderer.SetPosition(1, endPos);
        
    }
}
