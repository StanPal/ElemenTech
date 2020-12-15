using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] private Animator _aiController;
    public int nodeIndex;

    private Transform target = null;
    private AIPlayerManager _manager = null;
    public float currentHP;
    public pickUp[] mkPickups;
    public Hero[] mHeros ;

    void Start()
    {
        //nodeIndex = 0;//get number
        currentHP = 100.0f;
        _manager = ServiceLocator.Get<AIPlayerManager>();
        _manager.AddAIPlayer(this);

        mHeros = GameObject.FindObjectsOfType<Hero>();
        mkPickups = GameObject.FindObjectsOfType<pickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentHP -= 10.0f;
        }
        // _aiController.SetFloat("distToTarget", Vector2.Distance(transform.position, target.position));
       
         _aiController.SetFloat("HP", currentHP);
        

    }

    public void Attack()
    {
        Debug.Log("hp > 40, attack");
    }

    public void Run()
    {
        Debug.Log("hp < 40, run");
    }

    public void setReturnTure()
    {
        _aiController.SetBool("return", true);
    }
    
    public void setReturnFalse()
    {
        _aiController.SetBool("return", false);
    }

    public void showHP()
    {
        Debug.Log(currentHP);
    }

    public void IsPlayerNearby()
    {
        _aiController.SetBool("isPlayerNearby", false);
    }

    public void findHealth()
    {
        Debug.Log("looking for health");
    }
}
