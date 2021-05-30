using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFlowManager : MonoBehaviour
{
    public GameManager gameManager;

    public bool stopping;
    public float stopTime;
    public float slowTime;
    public float slowdownFactor;

    public float velocityThreshold;
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void TimeStop()
    {
        if (!stopping)
        {
            stopping = true;
            Time.timeScale = 0;

            StartCoroutine(nameof(Stop));
        }
    }

    private void Update()
    {
        if (gameManager.isGamePaused) return;
        Time.timeScale += (1f / slowTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = slowdownFactor;
        
        //yield return new WaitForSecondsRealtime(slowTime);
        //Time.timeScale = 1;
        stopping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.isGamePaused) return;

        if (rb.velocity.magnitude > velocityThreshold)
            TimeStop();
    }
}
