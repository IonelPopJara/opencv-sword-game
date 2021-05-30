using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSoundsManager : MonoBehaviour
{
    public Rigidbody swordRigidbody;
    public float soundThreshold;
    public float swingMaxTime;

    public bool isMoving;
    public AudioSource audioSource;
    public AudioClip[] swingSounds;

    public bool haveLost;

    private void Start()
    {
        isMoving = false;
        haveLost = false;
    }

    private void Update()
    {
        if (haveLost) return;
        float swordVelocity = swordRigidbody.velocity.magnitude;

        if (!isMoving && swordVelocity >= soundThreshold)
        {
            isMoving = true;
            int randomSound = Random.Range(0, swingSounds.Length);
            audioSource.clip = swingSounds[randomSound];
            audioSource.Play();
            Invoke(nameof(ResetSwing), swingMaxTime);
        }
    }

    void ResetSwing()
    {
        isMoving = false;
    }
}
