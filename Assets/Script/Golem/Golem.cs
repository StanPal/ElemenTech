using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    float mMaxHealth = 100.0f;
    [SerializeField]
    float mCurrentHealth;

    /// <summary>
    /// ///////////////////////////////////////////
    /// </summary>
    [SerializeField]
    private float shootingInterval;
    private float currentTime;
   // [SerializeField]
   // private float jumpingInterval;
   // private float currentTimeForJump;

    [SerializeField]
    private float jumpTime;
    private float jumpTimeCounter;
    public GameObject Sword;
    public GameObject longSword;
    public GameObject gun;
    public Rigidbody2D bullet;
    public Transform muzzle;
    public float bulletSpeed;

    [SerializeField]
    private float FasterMoveSpeed;
    [SerializeField]
    private float NormalMoveSpeed;
    [SerializeField]
    private float SlowerMoveSpeed;

    public float mSpeed;

    [SerializeField]
    private float mTime;

    [SerializeField]
    private GolemData.elementType mGolemType;

    private GolemData.attackType mAttackType;

    public bool reverse = false;

    public bool selfGeren = false;

    public float MaxHealth { get { return mMaxHealth; } }
    public float CurrentHealth { get { return mCurrentHealth; } }
    /////////////////////////////////////////////////
    GameObject teamOneObj;
    Transform teamOnePos;

    GameObject teamTwoObj;
    Transform teamTwoPos;
    [SerializeField]
    float mJumpForce;

    private Transform deadPosition;

    public GameObject Drop;

    private void Awake()
    {
        jumpTimeCounter = jumpTime;
        currentTime = shootingInterval;
       // currentTimeForJump = jumpingInterval;
        rb = GetComponent<Rigidbody2D>();
        mCurrentHealth = mMaxHealth;
        initGolemFeature();

        teamOneObj = GameObject.FindWithTag("Team1");
        teamOnePos = teamOneObj.transform;

        teamTwoObj = GameObject.FindWithTag("Team2");
        teamTwoPos = teamTwoObj.transform;
        ////   
    }

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Q))
        // {
        //     Rigidbody2D temp = Instantiate(bullet, muzzle.position, transform.rotation);
        //     if (!reverse)
        //     {
        //         temp.velocity = new Vector2(bulletSpeed, 0);
        //     }
        //     else
        //     {
        //         temp.velocity = new Vector2(-bulletSpeed, 0);
        //     }
        // }

        KillEnemy();
        Move();
        Shoot();
        SelfRegen();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            reverse = !reverse;
            Debug.Log("hit the block");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0);
        }
        if (coll.gameObject.tag == "Enemy")
        {
            reverse = !reverse;
            Debug.Log("hit the block");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0);
        }
        if (coll.gameObject.tag == "Team1")
        {
            reverse = !reverse;
            Debug.Log("hit the Hero");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0);
        }
        if (coll.gameObject.tag == "Team2")
        {
            reverse = !reverse;
            Debug.Log("hit the Hero");
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 0);
        }
    }

    private void initGolemFeature()
    {
        if (mGolemType == GolemData.elementType.Air)
        {
            mAttackType = GolemData.attackType.Ranged;
            Debug.Log("init Air bot attack to ranged");
            mSpeed = NormalMoveSpeed;
            Sword.SetActive(false);
            longSword.SetActive(false);

        }
        else if (mGolemType == GolemData.elementType.Earth)
        {
            mAttackType = GolemData.attackType.Melee;
            Debug.Log("init Earth bot attack to Melee");
            mSpeed = SlowerMoveSpeed;
            mCurrentHealth = 2 * mMaxHealth;
            longSword.SetActive(false);
            gun.SetActive(false);
        }
        else if (mGolemType == GolemData.elementType.Fire)
        {
            mAttackType = GolemData.attackType.Ranged;
            Debug.Log("init Fire bot attack to Ranged");
            mSpeed = NormalMoveSpeed;
            Sword.SetActive(false);
            longSword.SetActive(false);
        }
        else if (mGolemType == GolemData.elementType.Water)
        {
            mAttackType = GolemData.attackType.midRanged;
            Debug.Log("init Water bot attack to midRanged");
            mSpeed = FasterMoveSpeed;
            selfGeren = true;
            Sword.SetActive(false);
            gun.SetActive(false);
        }
    }
    private void Move()
    {
        if (!reverse)
        {
            transform.Translate(new Vector2(mSpeed, 0) * Time.deltaTime);
            //rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
        }
        else
        {
            transform.Translate(new Vector2(-mSpeed, 0) * Time.deltaTime);
        }
    }

    private void Jump()
    {
        
        if (jumpTimeCounter > 0.0f)
        {
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Jump");
            if (teamTwoPos.position.y - 1.0f > this.transform.position.y)
            {
                rb.velocity = Vector2.up * mJumpForce;
                jumpTimeCounter = jumpTime;
            }
            else if(teamOnePos.position.y - 1.0f > this.transform.position.y)
            {
                rb.velocity = Vector2.up * mJumpForce;
                jumpTimeCounter = jumpTime;
            }
        }
    }

    private void Shoot()
    {
        if (mAttackType == GolemData.attackType.Ranged)
        {
            if (currentTime > 0.0f)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = shootingInterval;
                Rigidbody2D mBullet = Instantiate(bullet, muzzle.position, transform.rotation);
                if (!reverse)
                {
                    mBullet.velocity = new Vector2(bulletSpeed, 0);
                }
                else
                {
                    mBullet.velocity = new Vector2(-bulletSpeed, 0);
                }

            }
        }
    }

    private void KillEnemy()
    {
        if (mCurrentHealth <= 0)
        {
            deadPosition = this.gameObject.transform;
            Destroy(this.gameObject);
            Instantiate(Drop, deadPosition.position, deadPosition.rotation);
        }
    }

    public void TakeDamage(float damage)
    {
        mCurrentHealth -= damage;
        Debug.Log("damage TAKEN");
    }

    public void SelfRegen()
    {
        if (selfGeren)
        {
            if (mCurrentHealth < MaxHealth && mTime > 1)
            {
                mCurrentHealth++;
                if (mCurrentHealth > MaxHealth)
                {
                    mCurrentHealth = MaxHealth;
                }
                mTime = 0;
            }
            else
            {
                mTime += Time.deltaTime;
            }
        }
    }
}





