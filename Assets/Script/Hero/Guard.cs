using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private HeroActions mHeroAction;
    private HeroMovement mHeroMovement;
    private HeroStats _HeroStats;
    public GameObject Shield;
    private GameObject mShield;
    private Elements.ElementalAttribute _TeammateElement;
    public Elements.ElementalAttribute TeamElement { set { _TeammateElement = value; } }
    private ComboSkillManager _SkillComboInfo;

    [SerializeField]
    private bool isGuarding = false;
    public bool Guarding { get { return isGuarding; } }
    [SerializeField]
    private float mGuardTime = 0.5f;
    [SerializeField]
    private float mShieldSize = 2.1f;
    private bool mShiendCreated = false;
    [SerializeField]
    private float mSpeedDecrease = 1f;
    [SerializeField]
    private float mShieldMaxEnergy = 100f;
    public float ShieldMaxEnergy { get { return mShieldMaxEnergy; } }
    [SerializeField]
    private float mShieldEnergy = 100f;
    public float ShieldEnergy { get { return mShieldEnergy; } set { mShieldEnergy = value; } }
    [SerializeField]
    private float mShieldEnergyTick = 0.2f;
    [SerializeField]
    private float mShieldRecoverAmount = 1f;
    public float ShieldRecoveryAmount { get { return mShieldRecoverAmount; } }
    [SerializeField]
    private float mShieldRecoveryTick = 3f;
    public float ShieldRecoveryTick { get { return mShieldRecoveryTick; } }
    public bool isShieldDisabled = false;


    private GameObject _ComboSkillPrefab;
    bool mSkillCombine = false;
    public bool ComboSkillOn { get { return mSkillCombine; } set { mSkillCombine = value; } }

    private void Start()
    {
        _SkillComboInfo = FindObjectOfType<ComboSkillManager>();
        mShieldEnergy = mShieldMaxEnergy;
        _HeroStats = GetComponentInParent<HeroStats>();
        mHeroAction = GetComponentInParent<HeroActions>();
        mHeroAction.onGuardPerformed += GuardMove;
        mHeroAction.onGuardExit += DestroyGuard;
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
            if (mShield == null)
            {
                Debug.Log("Cannot Create Shield, No Element Attached");
            }
            else
            {
                mShield.transform.position = Vector3.MoveTowards(mShield.transform.position, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), 1.0f);
                if (mShieldEnergy > 0)
                {
                    mShieldEnergy -= Time.deltaTime / mShieldEnergyTick;
                }
                else
                {
                    isShieldDisabled = true;
                }
                Color color = mShield.GetComponent<SpriteRenderer>().color;
                color.a = (mShieldEnergy * 0.01f);
                mShield.GetComponent<SpriteRenderer>().color = color;
            }
        }
        if(isGuarding && ComboSkillOn)
        {
            //_ComboSkillPrefab = _SkillComboInfo.ScriptableComboSkill.GetPrefabForCombo(_HeroStats.GetElement, _TeammateElement);
             _ComboSkillPrefab = Instantiate(_SkillComboInfo.ScriptableComboSkill.GetPrefabForCombo(_HeroStats.GetElement, _TeammateElement), transform);
            //if (_ComboSkillPrefab.GetComponent<SteamCover>().NeedQuaternion)
            //{
            //    _ComboSkillPrefab = Instantiate(_SkillComboInfo.ScriptableComboSkill.GetPrefabForCombo(_HeroStats.GetElement, _TeammateElement), transform.position, Quaternion.identity);
            //}
            //else
            //{
            //    _ComboSkillPrefab = Instantiate(_SkillComboInfo.ScriptableComboSkill.GetPrefabForCombo(_HeroStats.GetElement, _TeammateElement), transform);
            //}
            _ComboSkillPrefab.tag = this.GetComponent<HeroStats>().tag;
           
            _TeammateElement = Elements.ElementalAttribute.None;
            //if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Water)
            //{
            //    GameObject ComboSkillClone = Instantiate(comboSkill, transform.position, Quaternion.identity);
            //    ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            //}
            //if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Earth)
            //{
            //    GameObject ComboSkillClone = Instantiate(comboSkill, transform);
            //    ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            //}
            //if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Fire)
            //{
            //    GameObject ComboSkillClone = Instantiate(comboSkill, transform);
            //    ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            //}
            //if (GetComponent<HeroStats>().GetElement == Elements.ElementalAttribute.Air)
            //{
            //    GameObject ComboSkillClone = Instantiate(comboSkill, transform);
            //    ComboSkillClone.tag = this.GetComponent<HeroStats>().tag;
            //}
            //Debug.Log(FindObjectOfType<Guard>().gameObject.transform.position);
            ComboSkillOn = false;
        }
    }
 
    private void DestroyGuard()
    {
        Destroy(mShield);
        isGuarding = false;
        mShiendCreated = false;
    }

    private void OnDestroy()
    {
        //mHeroAction.onGuardPerformed -= GuardMove;
        //mHeroAction.onGuardExit -= DestroyGuard;
    }

    private void SummonGuard()
    {
        if (!mShiendCreated && !isShieldDisabled)
        {
            mShield = Instantiate(Shield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), Quaternion.identity);
            mShield.transform.localScale = new Vector3(mShieldSize,mShieldSize,mShieldSize);
            Color color = mShield.GetComponent<SpriteRenderer>().color;
            color.a = (mShieldEnergy * 0.01f);
            mShield.GetComponent<SpriteRenderer>().color = color;
            mShiendCreated = true;
        }
    }    
}
