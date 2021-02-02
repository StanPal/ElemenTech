using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] public Animator _aiController;
    public int nodeIndex;

    private Transform target = null;
    private AIPlayerManager _manager = null;
    public float currentHP;
    public pickUp[] mkPickups;
    public Hero[] mHeros ;
    public Hero nearestHero;
    public Hero nearestHealth;

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
        currentHP = this.GetComponent<Golem>().CurrentHealth;
        _aiController.SetFloat("HP", currentHP);
    }

    public void Attack()
    {
        Debug.Log("hp > 40, attack the players");
    }

    public void Run()
    {
        Debug.Log("hp < 40, try to run away and looking for health");
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
        foreach(var t in mHeros)
        {
            if(Mathf.Abs((t.GetComponent<Hero>().transform.position.x - this.GetComponent<Transform>().position.x)) < 5 
                && Mathf.Abs((t.GetComponent<Hero>().transform.position.y - this.GetComponent<Transform>().position.y)) < 5)
            {
                _aiController.SetBool("isPlayerNearby", true);
                nearestHero = t;
                return;
            }
        }
        _aiController.SetBool("isPlayerNearby", false);
    }
    public void IsPickUpNearby()
    {
        foreach (var t in mkPickups)
        {
            if(Mathf.Abs((t.GetComponent<pickUp>().transform.position.x - this.GetComponent<Transform>().position.x)) < 0.5f
                && Mathf.Abs((t.GetComponent<pickUp>().transform.position.y - this.GetComponent<Transform>().position.y)) < 0.5f)
            {
                _aiController.SetBool("isPickupNearby", true);
                return;
            }
        }
        _aiController.SetBool("isPlayerNearby", false);
    }

    public void findHealth()
    {
        Debug.Log("Move to the health position");

        float step = this.GetComponent<Golem>().mSpeed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, mkPickups[0].transform.position, step);
    }

    public void dashAway()
    {
        Debug.Log("run away from players!");
        if(nearestHero.GetComponent<Hero>().transform.position.x - this.GetComponent<Transform>().position.x < 5)
        {
            transform.Translate(new Vector2(-this.GetComponent<Golem>().mSpeed, 0) * Time.deltaTime);
        }                  
    }
}
