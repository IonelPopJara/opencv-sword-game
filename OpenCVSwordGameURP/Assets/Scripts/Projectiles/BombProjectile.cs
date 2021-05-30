using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour, IProjectile
{
    public GameObject explosionPrefab;
    public float radius = 5.0f;
    public float power = 10.0f;

    public LayerMask objectsAffectedByExplosion;

    //private void OnDestroy()
    //{
    //    Debug.Log("BOMB");
    //    GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //    Destroy(explosion, 1.5f);
    //}

    public void Explode()
    {
        Debug.Log("BOMB");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, objectsAffectedByExplosion);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
            }
        }
    }
}
