using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private float duration = 2.0f;


    private void Start()
    {
        Invoke("DestroyProjectile", duration);
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

}
