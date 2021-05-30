using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceProjectile : MonoBehaviour, IProjectile
{
    public GameObject explosionParticles;

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Debug.Log("Swoosh");
    }
}
