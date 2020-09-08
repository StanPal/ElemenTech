using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public event System.Action onSkillPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;

    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack;
    [SerializeField]
    float mMaxHealth;
    float mCurrentHealth;
    bool isLeft = false;
    public bool GetIsLeft { get { return isLeft; } }

    public float CurrentHealth { get { return mCurrentHealth; } }
    public float MaxHealth { get { return mMaxHealth; } }



    [SerializeField]
    float mSpeed;
    private float mMoveInput;
    private Rigidbody2D rb;
    Vector2 mPosition = Vector2.zero;
    private bool IsPressDown = false;
    
    [SerializeField]
    float mJumpForce;
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    float checkRadius;
    bool isGrounded;
    bool isJumping;
    [SerializeField]
    float jumpTime;
    float jumpTimeCounter;
    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    Elements.ElementalAttribute mElementalType;
    public Elements.ElementalAttribute GetElement { get { return mElementalType; } }

    [SerializeField]
    Transform arrowPosition;
    public GameObject projectile;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCurrentHealth = mMaxHealth;
    }

    void FixedUpdate()
    {
        //move
        mMoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(mMoveInput * mSpeed, rb.velocity.y);
        //Flip the character
        Vector3 characterScale = transform.localScale;
        if (rb.velocity.x < 0)
        {
            characterScale.x = -1;
            isLeft = true;
        }
        if (rb.velocity.x > 0)
        {
            characterScale.x = 1;
            isLeft = false;
        }
        transform.localScale = characterScale;
    }

    void Update()
    {
        if (mCurrentHealth <= 0)
        {
            HeroDie();
        }
        

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            isGrounded = false;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * mJumpForce;
        }
        if (Input.GetKey(KeyCode.Space)&& isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * mJumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rangeAttack();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            IsPressDown = true;
            onSkillPerformed.Invoke();
        }
        else
        {
            IsPressDown = false;
        }


//<<<<<<< HEAD

        // Player health down
        KillPlayerSelf();
//=======
        if (Input.GetKeyDown(KeyCode.P))
        {
            onPausePeformed.Invoke();
        }
        if (Input.GetKey(KeyCode.G))
            onGuardPerformed.Invoke();
        //else
        //    onGuardExit.Invoke();

//>>>>>>> master
    }

    void HeroDie()
    {
//<<<<<<< HEAD
       Destroy(gameObject);
       FindObjectOfType<GameManager>().EndGame();
//=======
       //Destroy(gameObject);
//>>>>>>> master
    }

    void rangeAttack()
    {
        Instantiate(projectile, arrowPosition.position, arrowPosition.rotation);
    }

    void KillPlayerSelf()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            mCurrentHealth--;
        }
    }
}
