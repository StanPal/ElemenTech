using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloem 
{
    public enum elementType
    {
        Air,
        Fire,
        Water,
        Earth     
    }

    public enum attackType
    {
        Ranged,
        Melee,
        midRanged
    }

    public elementType mElementType;
    public attackType mAttackType;

    public int mHP;
    public int mDefence;
    public int mAttack;
    public float fastSpeed;
    public float normalSpeed;
    public float slowSpeed;
    public float mSpeed;

    //check in the movement()
    public bool Fly = false;

    //check in Attack()
    public bool reflectiveBarriers = false;

    //check in Attack()
    public bool stun = false;

    //check in Attack()
    public bool bleed = false;

    //check in Attack()
    public bool splashDamage = false;

    //update()
    public bool selfRegen = false;

    public Gloem(elementType mType)
    {
        if(mType == elementType.Air)
        {
            mSpeed = normalSpeed;
            mAttackType = attackType.Ranged;
            Fly = true;
            reflectiveBarriers = true;

        }
        else if(mType == elementType.Earth)
        {
            mSpeed = slowSpeed;
            mAttackType = attackType.Melee;
            //higher Health/Armor
            stun = true;
        }
        else if (mType == elementType.Fire)
        {
            //Normal Left & Right movement
            mAttackType = attackType.Ranged;
            bleed = true;
            splashDamage = true;
        }
        else if (mType == elementType.Water)
        {
            mSpeed = fastSpeed;
            mAttackType = attackType.midRanged;
            selfRegen = true;
        }
    }
    void Jump()
    {

    }

    void Movement()
    {

    }

    void Attack()
    {

    }
}
