using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonManager : MonoBehaviour
{
    public List<Cannon> cannonsList;
    public TextMeshProUGUI projectilesLeftText;
    public int ProjectilesLeft;

    public int cannonsLeft;

    private void Start()
    {
        ProjectilesLeft = 0;

        foreach (Cannon cannon in cannonsList)
        {
            ProjectilesLeft += cannon.ProjectilesQueue.Count;
        }
    }

    private void Update()
    {
        cannonsLeft = cannonsList.Count;
        ProjectilesLeft = 0;

        Cannon removeCannon = null;

        foreach (Cannon cannon in cannonsList)
        {
            ProjectilesLeft += cannon.ProjectilesQueue.Count;
            if(cannon.ProjectilesQueue.Count <= 0)
            {
                Debug.Log("Remove cannon from list");
                removeCannon = cannon;
            }
        }

        if (removeCannon != null) cannonsList.Remove(removeCannon);

        projectilesLeftText.text = $"Projectiles left: {ProjectilesLeft}";

        if (ProjectilesLeft == 0)
            StartCoroutine(FindObjectOfType<GameManager>().PrepareToClearLevel());
    }

    public void ActivateCannons()
    {
        foreach (Cannon cannon in cannonsList)
        {
            cannon.canShoot = true;
        }
    }

    public void DeactivateCannons()
    {
        foreach (Cannon cannon in cannonsList)
        {
            cannon.canShoot = false;
        }
    }
}
