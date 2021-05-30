using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHealth : MonoBehaviour
{
    public GameObject destroyParticlesPrefab;
    public GameObject cannonModel;
    public Cannon cannon;

    public int twentyPercent;
    public bool isFunctioning;

    private void Start()
    {
        cannon = GetComponent<Cannon>();
        isFunctioning = true;
    }

    private void Update()
    {
        twentyPercent = (cannon.ProjectilesQueue.Count / 10) * 2;

        if(cannon.ProjectilesQueue.Count <= 0 && isFunctioning)
        {
            isFunctioning = false;
            Invoke(nameof(DeactivateCannon), 3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (cannon.ProjectilesQueue.Count <= 0) return;

        for (int i = 0; i <= twentyPercent; i++)
        {
            cannon.ProjectilesQueue.Dequeue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cannon.ProjectilesQueue.Count <= 0) return;

        for (int i = 0; i <= twentyPercent; i++)
        {
            cannon.ProjectilesQueue.Dequeue();
        }
    }

    private void DeactivateCannon()
    {
        cannonModel.SetActive(false);
        Instantiate(destroyParticlesPrefab, transform.position, Quaternion.identity);
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(0.2f, 0.2f));
    }
}
