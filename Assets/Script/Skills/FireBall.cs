using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float mDamage = 10.0f;
    private CanonBall canonball;
    private FireSkills fireSkills;

    private void Awake()
    {
        fireSkills = FindObjectOfType<FireSkills>();

    }
    // Start is called before the first frame update
    void Start()
    {     
        canonball = FindObjectOfType<CanonBall>();
     
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.transform.position.x + 10, this.transform.position.y), step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Golem>())
        {
            Golem golem = collision.GetComponent<Golem>();
            golem.TakeDamage(fireSkills.Damage);
            Destroy(gameObject);
        }
        else if(collision.GetComponent<Guard>())
        {      
            Guard guard = collision.GetComponent<Guard>();
            if (guard.Guarding)
            {
                Debug.Log("Shield Hit");
                collision.GetComponent<Guard>().ComboSkillOn = true;
            }
            Destroy(gameObject);
        }
        else if (collision.GetComponentInChildren<Wall>())
        {
            Destroy(gameObject);
        }
        else if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<Hero>().TakeDamage(mDamage);
        }
    }
}
