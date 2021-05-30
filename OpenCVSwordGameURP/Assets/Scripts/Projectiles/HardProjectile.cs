using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardProjectile : MonoBehaviour, IProjectile
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        if(collision.collider.gameObject.layer == 9)
        {
            gameObject.layer = 16;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }
}
