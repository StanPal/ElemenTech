using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSkills : MonoBehaviour
{
    // Earth Skills
    public GameObject mEarthSpike;
    [SerializeField]
    private int mNumSpikes = 3;
    [SerializeField]
    private int mSpikeHorizontalOffset = 1;
    [SerializeField]
    private float mSkillDuration = 0.5f;
    [SerializeField]
    private float mSpikeHeight = 1.0f;
    [SerializeField]
    private float mDamage = 10.0f;
    public float Damage { get { return mDamage; } }
    private bool mIsSpikeUp = true;
    [SerializeField]
    private List<GameObject> mEarthSpikeList = new List<GameObject>();

    PlayerSkills mHeroSkills;
    public PlayerSkills PlayerSkills { get { return mHeroSkills; } }
    private void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onEarthSkillPerformed += EarthSlam;
    }

    // Update is called once per frame
    void Update()
    {
        if (mHeroSkills.SkillActive)
            StartCoroutine("EarthSpikes");
    }

    IEnumerator EarthSpikes()
    {
        if (mIsSpikeUp)
            EarthSpikesUp();
        yield return new WaitForSeconds(mSkillDuration);
        EarthSpikesDown();
        yield return new WaitForSeconds(mSkillDuration * 2);
        ClearSpikes();
    }

    void EarthSlam()
    {        
            for (int i = 0; i < mNumSpikes; ++i)
            {
                GameObject mSpike;
                if (mHeroSkills.HeroAction.HeroMovement.GetIsLeft)
                {
                    mSpike = Instantiate(mEarthSpike, new Vector3(mHeroSkills.HeroMovement.transform.position.x - (mSpikeHorizontalOffset + i),
                                        mHeroSkills.HeroMovement.transform.position.y - 0.5f, 0.0f), Quaternion.identity);
                }
                else
                {
                    mSpike = Instantiate(mEarthSpike, new Vector3(mHeroSkills.HeroMovement.transform.position.x + (mSpikeHorizontalOffset + i),
                                        mHeroSkills.HeroMovement.transform.position.y - 0.5f, 0.0f), Quaternion.identity);
                }
                mEarthSpikeList.Add(mSpike);
            }
            mHeroSkills.SkillActive = true;
            mIsSpikeUp = true;
    }

    void EarthSpikesUp()
    {
        for (int i = 0; i < mEarthSpikeList.Count; ++i)
        {
            mEarthSpikeList[i].transform.Translate(new Vector3(0.0f, mSpikeHeight) * Time.deltaTime);
        }

    }
    void EarthSpikesDown()
    {
        mIsSpikeUp = false;
        for (int i = 0; i < mEarthSpikeList.Count; ++i)
        {
            mEarthSpikeList[i].transform.Translate(new Vector3(0.0f, -mSpikeHeight) * Time.deltaTime);
        }
    }

    void ClearSpikes()
    {
        for (int i = 0; i < mEarthSpikeList.Count; ++i)
        {
            Destroy(mEarthSpikeList[i]);
            mEarthSpikeList.Remove(mEarthSpikeList[i]);
        }
        mHeroSkills.SkillActive = false;
    }
}