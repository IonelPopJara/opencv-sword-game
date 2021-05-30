using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public bool isGrounded;
    public bool playerLoose;

    public float torque;
    public Rigidbody rb;
    public Vector3 randomDir;

    public SwordSoundsManager swordSounds;
    public AudioSource fallingSource;

    private void Start()
    {
        isGrounded = true;
        playerLoose = false;
        rb = GetComponent<Rigidbody>();

        randomDir = new Vector3(-1, Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void Update()
    {
        int layerMask = 1 << 7;

        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, groundCheck.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            isGrounded = true;
            Debug.DrawRay(groundCheck.position, groundCheck.TransformDirection(Vector3.down) * hit.distance, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(groundCheck.position, groundCheck.TransformDirection(Vector3.down) * 1000, Color.red);
        }

        if(!isGrounded && !playerLoose)
        {
            swordSounds.haveLost = true;
            playerLoose = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.freezeRotation = false;
            OnLooseRotate();
            fallingSource.Play();
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void OnLooseRotate()
    {
        //Vector3 randomDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        rb.AddTorque(randomDir * torque, ForceMode.Impulse);
    }
}
