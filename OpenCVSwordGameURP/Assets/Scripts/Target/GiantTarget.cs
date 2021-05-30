using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantTarget : MonoBehaviour
{
    public CannonManager cannonManager;
    public int twentyPercent;
    public int randomCannon;

    private void Start()
    {
        cannonManager = GameObject.FindObjectOfType<CannonManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cannonManager.cannonsList.Count <= 0) return;
        Debug.Log("Entraste al target");

        randomCannon = Random.Range(0, cannonManager.cannonsList.Count);

        float twentyPercent = (cannonManager.cannonsList[randomCannon].ProjectilesQueue.Count / 10) * 2;

        if (cannonManager.cannonsList[randomCannon].ProjectilesQueue.Count <= 0) return;

        for (int i = 0; i <= twentyPercent; i++)
        {
            cannonManager.cannonsList[randomCannon].ProjectilesQueue.Dequeue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
