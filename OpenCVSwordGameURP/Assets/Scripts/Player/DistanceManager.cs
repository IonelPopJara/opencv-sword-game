using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    // Deactivate manager when gamemanager lost
    public Transform player;
    public int distanceToPlayer;
    public float distanceThreshold;

    public Animator cuidaoAnimator;

    int previousDistance;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        previousDistance = 0;
    }

    private void Update()
    {
        gameOver = FindObjectOfType<GameManager>().isGameOver;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToPlayer = (int)(transform.position - player.position).magnitude;
        if(distanceToPlayer <= distanceThreshold)
        {
            if(previousDistance != distanceToPlayer)
            {
                if (gameOver) return;
                cuidaoAnimator.SetTrigger("Cuidao");
                Debug.Log("CUIDADO");
            }

            previousDistance = distanceToPlayer;
        }
    }
}
