using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TeamIndicator : MonoBehaviour
{
    // 1. 始终跟随着角色
    // 2. 根据角色的team变换颜色
   
    private SpriteRenderer spriteRenderer;
    //public Transform Target;
    public HeroStats team;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //var dir = Target.position - transform.position;
        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team1"))
        {
           // GetComponent<HeroStats>().team == HeroStats.TeamSetting.Team1
                spriteRenderer.color = Color.blue;
        }
        

        if (GetComponentInParent<HeroStats>().gameObject.tag.Equals("Team2"))
        {
            //GetComponent<HeroStats>().team == HeroStats.TeamSetting.Team2
                spriteRenderer.color = Color.red;
        }
    }
}
