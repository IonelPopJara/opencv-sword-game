using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * Send a signal to the cannon to fire after a dequeue
 */

[System.Serializable]
public class ProjectileData
{
    public float pForce;
    public float pSpawnTime;
    public ProjectileType pType;
}

public enum ProjectileType
{
    SLICEABLE,
    NONSLICEABLE,
    BOMB
}
public class Cannon : MonoBehaviour
{
    public GameObject[] bulletTypes;
    public ProjectileData[] ProjectilesData;
    
    private CannonShot cannonShot;
    public Queue<ProjectileData> ProjectilesQueue;

    public bool canShoot;

    [Header("Debug")]
    public float currentTime;
    public float timer;

    private void Awake()
    {
        ProjectilesQueue = new Queue<ProjectileData>();

        // Fill the queue
        foreach (var projectile in ProjectilesData)
        {
            ProjectilesQueue.Enqueue(projectile);
        }

        canShoot = false;
    }

    private void Start()
    {
        currentTime = ProjectilesQueue.Peek().pSpawnTime;
        cannonShot = GetComponent<CannonShot>();
    }

    private void Update()
    {
        if (!canShoot) return;

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            if (ProjectilesQueue.Count > 0)
            {
                ProjectileData projectileToShot = ProjectilesQueue.Dequeue();

                timer = projectileToShot.pSpawnTime;

                var bulletType = bulletTypes[(int)projectileToShot.pType];

                cannonShot.Shoot(bulletType, projectileToShot.pForce);
            }

            if(ProjectilesQueue.Count > 0)
            {
                if (ProjectilesQueue.Peek() != null)
                    currentTime = ProjectilesQueue.Peek().pSpawnTime;
            }
        }
    }
}
