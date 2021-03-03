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
        if (GetComponentInParent<HeroStats>()._elementalType == Elements.ElementalAttribute.Water)
        {

                spriteRenderer.color = Color.blue;
        }
        if (GetComponentInParent<HeroStats>()._elementalType == Elements.ElementalAttribute.Air)
        {

            spriteRenderer.color = Color.white;
        }
        if (GetComponentInParent<HeroStats>()._elementalType == Elements.ElementalAttribute.Fire)
        {

            spriteRenderer.color = Color.red;
        }
        if (GetComponentInParent<HeroStats>()._elementalType == Elements.ElementalAttribute.Earth)
        {
            spriteRenderer.color = Color.black;
        }
    }
}
