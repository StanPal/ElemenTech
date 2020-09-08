using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    [SerializeField]
    float mDamage = 50;
    private Vector3 targetPos;
    [SerializeField]
    private float targetXpos = 20f;
    private float targetYpos = 10f;

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float mArcHeight = 1;

    private Vector3 nextPos;
    private Vector3 startPos;


    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
        targetPos = new Vector3(transform.position.x + targetXpos, transform.position.y + targetYpos);
    }

    void Update()
    {
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = mArcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        // Do something when we reach the target
        if (nextPos == targetPos) Arrived();
    }

    void Arrived()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Wall>())
            Destroy(gameObject);
        else if(collision.GetComponent<Golem>())
        {
            collision.GetComponent<Golem>().TakeDamage(mDamage);
        }
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
