using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    string mName;
    [SerializeField]
    float mAttack;
    [SerializeField]
    float mMaxHealth;
    float mCurrentHealth;
    [SerializeField]
    Vector2 mPosition;


    // Start is called before the first frame update
    void Start()
    {
        mCurrentHealth = mMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {

    }
}
