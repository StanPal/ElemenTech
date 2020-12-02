﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserFireDistance;
    [SerializeField]
    private float defDistanceRay = 100;
    private LineRenderer mLineRenderer;

    private RaycastHit2D hit;
    [SerializeField]
    private RaycastHit2D[] raycastHits = null;

    private Guard guard;
    [SerializeField]
    private float mDamageTick = 3f;
    [SerializeField]
    private float mDamage = 10f;
    private float mTotalTime = 0;
    private void Awake()
    {
        transform.position = GetComponentInParent<HeroMovement>().transform.position;
        mLineRenderer = GetComponent<LineRenderer>();
        guard = GetComponentInParent<Guard>();
        raycastHits = new RaycastHit2D[10];

    }
    
    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        if (guard.Guarding)
        {
            if (GetComponentInParent<HeroMovement>().GetIsLeft)
            {
                raycastHits = Physics2D.RaycastAll(transform.position, transform.right * -1, Mathf.Infinity);
            }
            else
            {
                raycastHits = Physics2D.RaycastAll(transform.position, transform.right, Mathf.Infinity);
            }
            Draw2DRay(transform.position, laserFireDistance.position);
            for (int i = 0; i < raycastHits.Length; ++i)
            {
                RaycastHit2D hit = raycastHits[i];
                if (hit.collider != null)
                {
                    mTotalTime += Time.deltaTime;
                    if (tag.Equals("Team1"))
                   { 
                        if (hit.collider.tag.Equals("Team2") && hit.collider.TryGetComponent<HeroStats>(out HeroStats hero))
                        {
                            if (mTotalTime > mDamageTick)
                            {
                                hero.TakeDamage(mDamage);
                                mTotalTime = 0;
                            }
                        }
                    }
                    else if (tag.Equals("Team2"))
                    {
                        if (hit.collider.tag.Equals("Team1") && hit.collider.TryGetComponent<HeroStats>(out HeroStats hero))
                        {
                            if(mTotalTime > mDamageTick)
                            {
                              hero.TakeDamage(10f);
                              mTotalTime = 0;
                            }
                        }
                    }
                    //else
                    //{
                    //    if (hit.collider.TryGetComponent<HeroStats>(out HeroStats hero))
                    //    {
                    //        hero.TakeDamage(10f);
                    //    }

                    //}
                   Debug.Log(hit.collider.name);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        mLineRenderer.SetPosition(0, startPos);
        mLineRenderer.SetPosition(1, endPos);
        
    }
}
