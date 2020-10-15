﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private HeroActions mHero;
    public GameObject Shield;
    private GameObject mShield;
    [SerializeField]
    private bool isGuarding = false;
    public bool Guarding { get { return isGuarding; } }
    [SerializeField]
    private float mGuardTime = 0.5f;
    [SerializeField]
    private float mShieldSize = 2.1f;
    private bool mShiendCreated = false;
    public GameObject comboSkill;

    bool mSkillCombine = false;
    public bool ComboSkillOn { get { return mSkillCombine; } set { mSkillCombine = value; } }

    private void Start()
    {
        mHero = GetComponentInParent<HeroActions>();
        mHero.onGuardPerformed += GuardMove;
        mHero.onGuardExit += DestroyGuard;
    }

    private void GuardMove()
    {
        isGuarding = true;
        SummonGuard();
    }

    private void Update()
    {

        if (isGuarding)
        {
            //Test to automatically have hero guarding

            if (mShield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            {
                mShield.transform.position = Vector3.MoveTowards(mShield.transform.position, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), 1.0f);
            }
        }
        if(isGuarding && ComboSkillOn)
        {
            Instantiate(comboSkill, transform);
            Debug.Log(FindObjectOfType<Guard>().gameObject.transform.position);
            ComboSkillOn = false;
        }
    }

 
    private void DestroyGuard()
    {
        Destroy(mShield);
        isGuarding = false;
        mShiendCreated = false;
    }


    private void SummonGuard()
    {
        if (!mShiendCreated)
        {
            mShield = Instantiate(Shield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), Quaternion.identity);             
            
            mShield.transform.localScale = new Vector3(mShieldSize,mShieldSize,mShieldSize);
            mShiendCreated = true;
        }
    }    
}
