//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WaterGun : MonoBehaviour
//{
//    [SerializeField]
//    float damage = 2;
//    [SerializeField]
//    float projectileSpeed = 5;
//    private Rigidbody2D rigidbody;
//    [SerializeField]
//    float exitTime = 2.0f;
//    private Hero hero;
//    WaterSkills waterSkills;
//    private void Awake()
//    {
//        rigidbody = GetComponent<Rigidbody2D>();
//        waterSkills = FindObjectOfType<WaterSkills>();
//        hero = FindObjectOfType<Hero>();
//        if (hero.GetIsLeft)
//        {
//            projectileSpeed = -projectileSpeed;
//        }

//    }


//    private void FixedUpdate()
//    {
//        if (exitTime <= 0.0f)
//        {
//            Destroy(gameObject);
//        }
//        exitTime -= Time.deltaTime;
//        rigidbody.velocity = transform.right * projectileSpeed;
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.GetComponent<Golem>())
//        {
//            Debug.Log("Trigger");
//            Golem golem = collision.gameObject.GetComponent<Golem>();
//            if (golem != null)
//            {
//                golem.TakeDamage(damage);
//                Destroy(gameObject);
//            }
//        }

//        if (waterSkills.PlayerSkills.Hero.tag.Equals("Team1"))
//        {
//            if (collision.tag.Equals("Team2"))
//            {
//                collision.GetComponent<Hero>().TakeDamage(damage);
//            }
//        }
//        if (waterSkills.PlayerSkills.Hero.tag.Equals("Team2"))
//        {
//            if (collision.tag.Equals("Team1"))
//            {
//                collision.GetComponent<Hero>().TakeDamage(damage);
//            }
//        }

//        if (collision.GetComponentInParent<Walls>())
//        {
//            Destroy(gameObject);
//        }
//    }
//}
