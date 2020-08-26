using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private Hero mHero;
    public GameObject mEarthShield;
    private GameObject mShield;
    [SerializeField]
    private bool isGuarding = false;
    [SerializeField]
    private float mGuardTime = 0.5f;

    private void Start()
    {
        mHero = GetComponentInParent<Hero>();
        mHero.onGuardPerformed += GuardMove;
    }

    private void GuardMove()
    {

        switch (mHero.GetElement)
        {
            case Elements.ElementalAttribute.Fire:
                break;
            case Elements.ElementalAttribute.Earth:
                SummonEarthGuard();
                break;
            case Elements.ElementalAttribute.Water:
                break;
            case Elements.ElementalAttribute.Air:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (isGuarding)
            StartCoroutine("GuardUp");

    }

    IEnumerator GuardUp()
    { 
        yield return new WaitForSeconds(mGuardTime);
        Destroy(mShield);
        isGuarding = false;
    }

    private void SummonEarthGuard()
    {
       mShield = Instantiate(mEarthShield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), Quaternion.identity);
       mShield.transform.localScale *= 2.1f;
        isGuarding = true;
    }
}
