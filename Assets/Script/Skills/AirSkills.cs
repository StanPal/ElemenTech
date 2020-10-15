//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AirSkills : MonoBehaviour
//{
//    public GameObject mAirJet;

//    PlayerSkills mHeroSkills;
//    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }

//    private void Start()
//    {
//        mHeroSkills = GetComponent<PlayerSkills>();
//        mHeroSkills.onAirSkillPerformed += AirJet;
//    }

//    void AirJet()
//    {
//        Instantiate(mAirJet, mHeroSkills.Hero.transform.position, Quaternion.identity);
//    }
//}
