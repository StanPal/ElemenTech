//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FireBall : MonoBehaviour
//{
//    [SerializeField]
//    private float speed = 10.0f;
//    [SerializeField]
//    private float mDamage = 10.0f;
//    private CanonBall canonball;
//    private FireSkills fireSkills;
//    [SerializeField]
//    Vector2 displacement;
//    [SerializeField]
//    float distance = 10;
//    private void Awake()
//    {
//        fireSkills = FindObjectOfType<FireSkills>();
//        displacement.Set(this.transform.position.x + distance, this.transform.position.y);
//        if (fireSkills.PlayerSkills.Hero.GetIsLeft)
//        {   
//            displacement.x *= -1;
//        }

//    }


//    private void Update()
//    {
//        float step = speed * Time.deltaTime;
//        transform.position = Vector2.MoveTowards(transform.position, displacement, step);
//        if (transform.position.Equals(displacement))
//            Destroy(this.gameObject);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.GetComponent<Golem>())
//        {
//            Golem golem = collision.GetComponent<Golem>();
//            golem.TakeDamage(fireSkills.Damage);
//            Destroy(gameObject);
//        }
//         if(collision.GetComponent<Guard>())
//        {      
//            Guard guard = collision.GetComponent<Guard>();
//            if (guard.Guarding)
//            {
//                Debug.Log("Shield Hit");
//                collision.GetComponent<Guard>().ComboSkillOn = true;
//                Destroy(gameObject);
//            }
//        }
//         if (collision.GetComponentInParent<Walls>())
//        {
//            Destroy(gameObject);
//        }
//        if (fireSkills.PlayerSkills.Hero.tag.Equals("Team1"))
//        {
//            if (collision.tag.Equals("Team2"))
//            {
//                collision.GetComponent<Hero>().TakeDamage(fireSkills.Damage);
//                Destroy(gameObject);
//            }
//        }
//        if (fireSkills.PlayerSkills.Hero.tag.Equals("Team2"))
//        {
//            if (collision.tag.Equals("Team1"))
//            {
//                collision.GetComponent<Hero>().TakeDamage(fireSkills.Damage);
//                Destroy(gameObject);
//            }
//        }
//    }
//}
