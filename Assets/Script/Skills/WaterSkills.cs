using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkills : MonoBehaviour
{
    public GameObject mWaterSpike;
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
    private bool mIsSpikeUp = true;
    [SerializeField]
    private List<GameObject> mWaterSpikeList = new List<GameObject>();

    PlayerSkills mHeroSkills;

    void Start()
    {
        mHeroSkills = GetComponent<PlayerSkills>();
        mHeroSkills.onEarthSkillPerformed += WaterSlam;
    }

    void Update()
    {
        if (mHeroSkills.SkillActive)
            StartCoroutine("WaterSpikes");
    }

    IEnumerator WaterSpikes()
    {
        if (mIsSpikeUp)
            WaterSpikesUp();
        yield return new WaitForSeconds(mSkillDuration);
        WaterSpikesDown();
        yield return new WaitForSeconds(mSkillDuration * 2);
        ClearSpikes();
    }

    void WaterSlam()
    {
        for (int i = 0; i < mNumSpikes; ++i)
        {
            GameObject mSpike = Instantiate(mWaterSpike, new Vector3(mHeroSkills.Hero.transform.position.x + (mSpikeHorizontalOffset + i), -1.0f, 0.0f), Quaternion.identity);
            mWaterSpikeList.Add(mSpike);
        }
        mHeroSkills.SkillActive = true;
        mIsSpikeUp = true;
    }

    void WaterSpikesUp()
    {
        for (int i = 0; i < mWaterSpikeList.Count; ++i)
        {
            mWaterSpikeList[i].transform.Translate(new Vector3(0.0f, mSpikeHeight) * Time.deltaTime);

        }
    }

    void WaterSpikesDown()
    {
        mIsSpikeUp = false;
        for (int i = 0; i < mWaterSpikeList.Count; ++i)
        {
            mWaterSpikeList[i].transform.Translate(new Vector3(0.0f, -mSpikeHeight) * Time.deltaTime);
        }

    }

    void ClearSpikes()
    {
        for (int i = 0; i < mWaterSpikeList.Count; ++i)
        {
            Destroy(mWaterSpikeList[i]);
            mWaterSpikeList.Remove(mWaterSpikeList[i]);
        }
        mHeroSkills.SkillActive = false;
    }

}
